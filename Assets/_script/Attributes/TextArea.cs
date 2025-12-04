using UnityEngine;

public class TextArea : MonoBehaviour
{
        [TextAreaAttribute]
        [SerializeField] private string _myTextArea;
        [SerializeField] private int _maxLines;
    
}
