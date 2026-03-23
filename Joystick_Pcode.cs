           if (Input.touches.Any(t => t.phase == TouchPhase.Moved))              // Are there any DRAG touches happening
            {
                var touch = Input.touches.First(t => t.phase == TouchPhase.Moved);    // Just get the first drag - ignore others

                // Speed (aka radius aka distance) from touch point to joystick center 

                if ((touch.position.x <= SPad_L_orig_Xa + SPad_size)     // Drag is only valid 
                && (touch.position.y <= SPad_L_orig_Yt + SPad_size))     // inside the SPad area
                {
                  datagram.Normalized_X = NormalizeToJoystick_X(touch.position.x);      // Between -1 and +1
                  datagram.Normalized_Y = NormalizeToJoystick_Y(touch.position.y);      // Between -1 and +1

                  datagram.Normalized_X = (touch.position.x - SPad_half)/SPad_half;      // Between -1 and +1
                  datagram.Normalized_Y = (touch.position.y - SPad_half)/SPad_half;      // Between -1 and +1
                }
             
            }
            else // no DRAG touches are happening, so we set x,y to zero
            {
                datagram.Normalized_X = 0f;             // Between -1 and +1
                datagram.Normalized_Y = 0f;             // Between -1 and +1
            }


//============================================================================== NORMALIZE JOYSTICK  X
public float NormalizeToJoystick_X(float touch_point_Xa)
{
  // working this out on graph paper...BRB
}

//============================================================================== NORMALIZE JOYSTICK  Y
public float NormalizeToJoystick_X(float touch_point_Yt)
{
  // working this out on graph paper...BRB
}


