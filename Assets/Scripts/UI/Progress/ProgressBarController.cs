using System;
using OrbReaper.UI.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour, IProgressBar
{
    public const string FillParameterName = "_Fill"; 
    
    [SerializeField]
    private Image image;
    
    [SerializeField]
    private float updateSpeed = 1.5f;
    
    [SerializeField]
    private float value = 0.5f;
    
    [SerializeField]
    private float maxValue = 1f;
    
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [SerializeField]
    private string fillParameterNameOnMaterial = FillParameterName;
    
    private Func<float, string> valueFormatter;
    
    private float targetValue = 0f;

    private Action<float> progressHandler;

    private float currentFill;
    
    private void Start()
    {
        if (image.material == null || fillParameterNameOnMaterial == null || !image.material.HasProperty(fillParameterNameOnMaterial))
        {
            progressHandler = fillAmount => image.fillAmount = fillAmount; 
        }
        else
        {
            image.fillAmount = 1f;
            progressHandler = fillAmount => image.material.SetFloat(fillParameterNameOnMaterial, fillAmount);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        currentFill = Mathf.MoveTowards(currentFill, targetValue, updateSpeed * Time.deltaTime);
        progressHandler.Invoke(currentFill);
    }

    public void SetFormatter(Func<float, string> valueFormatter)
    {
        this.valueFormatter = valueFormatter;
    }

    public float Value
    {
        get => value;

        set
        {
            if (Mathf.Approximately(this.value, value))
            {
                return;
            }
            
            this.value = value;
            UpdateValue();
        }
    }

    public float MaxValue
    {
        get => maxValue;
        
        set
        {
            if (Mathf.Approximately(this.value, value))
            {
                return;
            }

            maxValue = value;
            UpdateValue();
        }
    }

    private void UpdateValue()
    {
        targetValue = Value / MaxValue;
        var valueFormatted = valueFormatter?.Invoke(Value) ?? Value.ToString("F0");
        var maxValueFormatted = valueFormatter?.Invoke(MaxValue) ?? MaxValue.ToString("F0");
        if (textMeshPro)
        {
            textMeshPro.text = $"{valueFormatted} / {maxValueFormatted}";
        }
    }
}