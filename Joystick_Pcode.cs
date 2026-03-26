

//================================================================= DATAGRAM
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct GamePadDatagram
{
    public int RandomGamePadID;              // Just has to be unique within the wifi network
    public float Normalized_X;               // Normalized between -1 and +1  (jostick as unit circle)
    public float Normalized_Y;               // Normalized between -1 and +1  (jostick as unit circle)
    private byte Button_flags;               // We store 4 booleans inside this single byte

    // Equality members
    public override bool Equals(object obj) => obj is GamePadDatagram other && Equals(other);

    public bool Equals(GamePadDatagram other)
    {
        return RandomGamePadID == other.RandomGamePadID &&
               Normalized_X == other.Normalized_X &&
               Normalized_Y == other.Normalized_Y &&
               Button_flags == other.Button_flags;
    }

    public override int GetHashCode()              // OK OH, gotta leave it in cus we DO override equals
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + RandomGamePadID.GetHashCode();
            hash = hash * 23 + Normalized_X.GetHashCode();
            hash = hash * 23 + Normalized_Y.GetHashCode();
            hash = hash * 23 + Button_flags.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(GamePadDatagram left, GamePadDatagram right) => left.Equals(right);
    public static bool operator !=(GamePadDatagram left, GamePadDatagram right) => !left.Equals(right);


    public void SetBool(int index, bool value)      // Helper methods to work with bools
    {
        if (value)
            Button_flags |= (byte)(1 << index);             // Set bit = 1
        else
            Button_flags &= (byte)~(1 << index);            // Clear bit = 0
    }
    public bool GetBool(int index) => (Button_flags & (1 << index)) != 0;
}



//================================================================== TEN COLORS
using System.Drawing;

Color[] Color_Index = new Color[]
{
    Color.FromArgb(230, 25, 75),   // Red - Vibrant
    Color.FromArgb(60, 180, 75),   // Green - Strong
    Color.FromArgb(255, 225, 25),  // Yellow - Bright
    Color.FromArgb(67, 99, 216),   // Blue - Primary
    Color.FromArgb(245, 130, 49),  // Orange - Vivid
    Color.FromArgb(145, 30, 180),  // Purple - Deep
    Color.FromArgb(66, 212, 244),  // Cyan - Neon
    Color.FromArgb(240, 50, 230),  // Magenta - Shocking
    Color.FromArgb(191, 239, 69),  // Lime - Electric
    Color.FromArgb(70, 153, 144)   // Teal - Deep
};



//================================================================== DRAG TOUCH - JOYSTICK
           if (Input.touches.Any(t => t.phase == TouchPhase.Moved))              // Are there any DRAG touches happening
            {
                var touch = Input.touches.First(t => t.phase == TouchPhase.Moved);    // Just get the first drag - ignore others

                // Speed (aka radius aka distance) from touch point to joystick center 

                if ((touch.position.x <= SPad_L_orig_Xa + SPad_size)     // Drag is only valid 
                && (touch.position.y <= SPad_L_orig_Yt + SPad_size))     // inside the SPad area
                {
                  datagram.Normalized_X = (touch.position.x - SPad_half)/SPad_half;      // Between -1 and +1
                  datagram.Normalized_Y = (touch.position.y - SPad_half)/SPad_half;      // Between -1 and +1
                }
             
            }
            else // no DRAG touches are happening, so we set x,y to zero
            {
                datagram.Normalized_X = 0f;             // Between -1 and +1
                datagram.Normalized_Y = 0f;             // Between -1 and +1
            }
