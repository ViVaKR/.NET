namespace VivRPG;

public struct Vector3
{
    public float x;
    public float y;
    public float z;

    public float magnitude;

    public readonly Vector3 Normalized
    {
        get => this;
    }

    // constructor code goes here
    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = y;
    }


}
