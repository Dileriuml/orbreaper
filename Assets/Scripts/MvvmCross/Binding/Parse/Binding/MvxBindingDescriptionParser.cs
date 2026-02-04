// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Binding.Binders;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Bindings.SourceSteps;
using MvvmCross.Binding.Combiners;
using MvvmCross.Binding.Parse.Binding.Lang;
using MvvmCross.Binding.Parse.Binding.Tibet;
using MvvmCross.Converters;

namespace MvvmCross.Binding.Parse.Binding
{
    public class MvxBindingDescriptionParser
        : IMvxBindingDescriptionParser
    {
        private IMvxBindingParser _bindingParser;
        private IMvxValueConverterLookup _valueConverterLookup;

        protected IMvxBindingParser BindingParser
        {
            get
            {
                _bindingParser ??= Mvx.IoCProvider.Resolve<IMvxBindingParser>();
                return _bindingParser;
            }
        }

        private IMvxLanguageBindingParser _languageBindingParser;

        protected IMvxLanguageBindingParser LanguageBindingParser
        {
            get
            {
                _languageBindingParser ??= Mvx.IoCProvider.Resolve<IMvxLanguageBindingParser>();
                return _languageBindingParser;
            }
        }

        protected IMvxValueConverterLookup ValueConverterLookup
        {
            get
            {
                _valueConverterLookup ??= Mvx.IoCProvider.Resolve<IMvxValueConverterLookup>();
                return _valueConverterLookup;
            }
        }

        protected IMvxValueConverter FindConverter(string converterName)
        {
            if (converterName == null)
                return null;

            var toReturn = ValueConverterLookup.Find(converterName);

            return toReturn;
        }

        protected IMvxValueCombiner FindCombiner(string combiner)
        {
            return MvxBindingSingletonCache.Instance.ValueCombinerLookup.Find(combiner);
        }

        public IEnumerable<MvxBindingDescription> Parse(string text)
        {
            var parser = BindingParser;
            return Parse(text, parser);
        }

        public IEnumerable<MvxBindingDescription> Parse(string text, IMvxBindingParser parser)
        {
            MvxSerializableBindingSpecification specification;
            if (!parser.TryParseBindingSpecification(text, out specification))
            {
                return Array.Empty<MvxBindingDescription>();
            }

            if (specification == null)
                return Array.Empty<MvxBindingDescription>();

            return from item in specification
                   select SerializableBindingToBinding(item.Key, item.Value);
        }

        public IEnumerable<MvxBindingDescription> LanguageParse(string text)
        {
            var parser = LanguageBindingParser;
            return Parse(text, parser);
        }

        public MvxBindingDescription ParseSingle(string text)
        {
            MvxSerializableBindingDescription description;
            var parser = BindingParser;
            if (!parser.TryParseBindingDescription(text, out description))
            {
                return null;
            }

            if (description == null)
                return null;

            return SerializableBindingToBinding(null, description);
        }

        private string GetErrorTextParameter(string text)
        {
            if (text == null)
                return string.Empty;

            if (text.Length > 20)
                return text.Substring(0, 20);

            return text;
        }

        public MvxBindingDescription SerializableBindingToBinding(
            string targetName, MvxSerializableBindingDescription description)
        {
            return new MvxBindingDescription
            {
                TargetName = targetName,
                Source = SourceStepDescriptionFrom(description),
                Mode = description.Mode,
            };
        }

        private MvxSourceStepDescription SourceStepDescriptionFrom(MvxSerializableBindingDescription description)
        {
            if (description.Path != null)
            {
                return new MvxPathSourceStepDescription()
                {
                    SourcePropertyPath = description.Path,
                    Converter = FindConverter(description.Converter),
                    ConverterParameter = description.ConverterParameter,
                    FallbackValue = description.FallbackValue
                };
            }

            if (description.Literal != null)
            {
                var literal = description.Literal;
                if (literal == MvxTibetBindingParser.LiteralNull)
                    literal = null;

                return new MvxLiteralSourceStepDescription()
                {
                    Literal = literal,
                    Converter = FindConverter(description.Converter),
                    ConverterParameter = description.ConverterParameter,
                    FallbackValue = description.FallbackValue
                };
            }

            if (description.Function != null)
            {
                // first look for a combiner with the name
                var combiner = FindCombiner(description.Function);
                if (combiner != null)
                {
                    return new MvxCombinerSourceStepDescription()
                    {
                        Combiner = combiner,
                        InnerSteps = description.Sources == null
                            ? new List<MvxSourceStepDescription>() :
                            description.Sources.Select(s => SourceStepDescriptionFrom(s)).ToList(),
                        Converter = FindConverter(description.Converter),
                        ConverterParameter = description.ConverterParameter,
                        FallbackValue = description.FallbackValue
                    };
                }

                // no combiner, then drop back to looking for a converter
                var converter = FindConverter(description.Function);

                if (description.Sources == null || description.Sources.Count == 0)
                {
                    return new MvxLiteralSourceStepDescription()
                    {
                        Literal = null,
                    };
                }

                if (description.Sources.Count > 2)
                {
                    return new MvxLiteralSourceStepDescription()
                    {
                        Literal = null,
                    };
                }
                    
                return new MvxCombinerSourceStepDescription()
                {
                    Combiner = new MvxValueConverterValueCombiner(converter),
                    InnerSteps = description.Sources.Select(source => SourceStepDescriptionFrom(source)).ToList(),
                    Converter = FindConverter(description.Converter),
                    ConverterParameter = description.ConverterParameter,
                    FallbackValue = description.FallbackValue
                };
            }

            // this probably suggests that the path is the entire source object
            return new MvxPathSourceStepDescription()
            {
                SourcePropertyPath = null,
                Converter = FindConverter(description.Converter),
                ConverterParameter = description.ConverterParameter,
                FallbackValue = description.FallbackValue
            };
        }
    }
}
