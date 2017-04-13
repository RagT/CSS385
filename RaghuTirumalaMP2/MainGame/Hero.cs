using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using XNACS1Lib;

namespace MainGame
{
    class Hero
    {
        XNACS1Circle boundCircle = null;
        public Hero(Vector2 location)
        {
            boundCircle = new XNACS1Circle(location, 3);
        }

        //Update Hero's position based on left stick input
        public void Update()
        {
            Vector2 leftStick = XNACS1Base.GamePad.ThumbSticks.Left;
            if(leftStick.Length() > 1.0f)
            {
                leftStick.Normalize();
            }
            boundCircle.Center += leftStick;
            XNACS1Base.World.ClampAtWorldBound(boundCircle);
        }

        public XNACS1Circle GetCircle()
        {
            return boundCircle;
        }
    }
}
