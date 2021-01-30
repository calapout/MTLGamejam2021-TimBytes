using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[SerializeField]
public class Int2Event : UnityEvent<int> { }

public class CodeGameNumber : MonoBehaviour
{

    public Text txtNumber;
    public int value = 0;

    public Int2Event OnValueChange = new Int2Event();

    public void Increment()
    {
        if (value != 9) { value = Mathf.Clamp(value + 1, 0, 9); }
        else { value = 0; }
        txtNumber.text = value.ToString();
        OnValueChange?.Invoke(value);
    }

    public void Decrement()
    {
        if (value != 0) { value = Mathf.Clamp(value - 1, 0, 9); }
        else { value = 9; }
        txtNumber.text = value.ToString();
        OnValueChange?.Invoke(value);
    }

}
