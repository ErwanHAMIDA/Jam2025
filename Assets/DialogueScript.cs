using UnityEngine;
public enum Dialogueoptn
{
    paskontant,
    midtier,
    content

}
public class DialogueScript : MonoBehaviour
{

    Dialogueoptn dialogueoptn;
    DialogueAsset asset;
    void Start()
    {
        Showdialogue();
    }
    public void Showdialogue()
    {
        int Dialogue = (int)dialogueoptn;
        Debug.Log(Dialogue);

    }
 
}
