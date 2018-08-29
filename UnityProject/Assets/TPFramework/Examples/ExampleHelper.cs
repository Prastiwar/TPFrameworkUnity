using UnityEngine;

public static class ExampleHelper
{
    public static WaitForSeconds WaitSecond = new WaitForSeconds(1);
    public static void MessageWithLines(string message)
    {
        DrawLine();
        Debug.Log(message);
        DrawLine();
    }

    public static void DrawLine()
    {
        Debug.Log("-------------------------------------------------------------");
    }
}
