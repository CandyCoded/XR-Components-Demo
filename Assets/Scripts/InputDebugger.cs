using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CandyCoded.XRComponents;
using UnityEngine;
using UnityEngine.UI;

public class InputDebugger : MonoBehaviour
{

    private Dictionary<Text, string> textComponentsWithTemplate = new Dictionary<Text, string>();

    private Dictionary<XRInput.Button, string> buttons = new Dictionary<XRInput.Button, string>()
    {
        { XRInput.Button.One, "One" },
        { XRInput.Button.Two, "Two" },
        { XRInput.Button.Three, "Three" },
        { XRInput.Button.Four, "Four" },
        { XRInput.Button.Start, "Start" },
        { XRInput.Button.PrimaryThumbstick, "PrimaryThumbstick" },
        { XRInput.Button.SecondaryThumbstick, "SecondaryThumbstick" }
    };

    private Dictionary<XRInput.Axis1D, string> axis1ds = new Dictionary<XRInput.Axis1D, string>() { { XRInput.Axis1D.PrimaryIndexTrigger, "PrimaryIndexTrigger" }, { XRInput.Axis1D.SecondaryIndexTrigger, "SecondaryIndexTrigger" }, { XRInput.Axis1D.PrimaryHandTrigger, "PrimaryHandTrigger" }, { XRInput.Axis1D.SecondaryHandTrigger, "SecondaryHandTrigger" } };

    private Dictionary<XRInput.Axis2D, string> axis2ds = new Dictionary<XRInput.Axis2D, string>() { { XRInput.Axis2D.PrimaryThumbstickHorizontal, "PrimaryThumbstickHorizontal" }, { XRInput.Axis2D.PrimaryThumbstickVertical, "PrimaryThumbstickVertical" }, { XRInput.Axis2D.SecondaryThumbstickHorizontal, "SecondaryThumbstickHorizontal" }, { XRInput.Axis2D.SecondaryThumbstickVertical, "SecondaryThumbstickVertical" } };

    private void Awake()
    {

        foreach (var textComponent in FindObjectsOfType<Text>())
        {

            textComponentsWithTemplate.Add(textComponent, textComponent.text);

        }

    }

    private void Update()
    {

        foreach (var button in buttons)
        {

            if (XRInput.Get(button.Key))
            {

                SetValueOfTextComponentTemplate(textComponentsWithTemplate.First(t => t.Value.StartsWith($"{button.Value}:")), "true");

            }
            else
            {

                SetValueOfTextComponentTemplate(textComponentsWithTemplate.First(t => t.Value.StartsWith($"{button.Value}:")), "false");

            }

        }

        foreach (var axis1d in axis1ds)
        {

            if (XRInput.Get(axis1d.Key))
            {

                SetValueOfTextComponentTemplate(textComponentsWithTemplate.First(t => t.Value.StartsWith($"{axis1d.Value}:")), "true");

            }
            else
            {

                SetValueOfTextComponentTemplate(textComponentsWithTemplate.First(t => t.Value.StartsWith($"{axis1d.Value}:")), "false");

            }

        }

        foreach (var axis2d in axis2ds)
        {

            var value = XRInput.Get(axis2d.Key);

            SetValueOfTextComponentTemplate(textComponentsWithTemplate.First(t => t.Value.StartsWith($"{axis2d.Value}:")), value.ToString(CultureInfo.InvariantCulture));

        }

    }

    private static void SetValueOfTextComponentTemplate(KeyValuePair<Text, string> textComponentWithTemplate, string value)
    {

        textComponentWithTemplate.Key.text = string.Format(textComponentWithTemplate.Value, value);

    }

}
