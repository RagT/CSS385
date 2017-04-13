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
    class Chaser
    {
        XNACS1Circle boundCircle = null;
        XNACS1Circle hero = null;
        const float TURN_RATE = 0.05f;
        int TickCount;

        public Chaser(XNACS1Circle hero)
        {
            this.hero = hero;
            boundCircle = new XNACS1Circle(new Vector2(1.5f, 1.5f), 3);
            boundCircle.ShouldTravel = true;
            boundCircle.Velocity = new Vector2(1.1f, 0);
            TickCount = 800;
            boundCircle.Texture = "shark";
        }

        public int Update()
        {
            if(!boundCircle.Collided(hero) && IsInWorldBounds() && TickCount > 0)
            {
                Vector2 dir = hero.Center - boundCircle.Center;
                dir.Normalize();
                float theta = MathHelper.ToDegrees((float)Math.Acos(
                    (double)(Vector2.Dot(boundCircle.FrontDirection, dir))));
                if(theta > 0.001f)
                {
                    Vector3 fIn3D = new Vector3(boundCircle.FrontDirection, 0f);
                    Vector3 tIn3D = new Vector3(dir, 0f);
                    Vector3 sign = Vector3.Cross(fIn3D, tIn3D);

                    boundCircle.RotateAngle += Math.Sign(sign.Z) * theta * TURN_RATE;
                    boundCircle.VelocityDirection = boundCircle.FrontDirection;
                }
                TickCount--;
                return 0; 
            }
            else
            {
                if(boundCircle.Collided(hero))
                {
                    Destroy();
                    XNACS1Base.PlayACue("die");
                    return 1; //hit hero
                }
                Destroy();
                XNACS1Base.PlayACue("avoided");
                return 2; //avoided                
            }
        }

        public void Destroy()
        {
            boundCircle.RemoveFromAutoDrawSet();
            boundCircle = null;
        }

        private bool IsInWorldBounds()
        {
            return ((boundCircle.CenterX) <= XNACS1Base.World.WorldMax.X)
            && ((boundCircle.CenterX) >= XNACS1Base.World.WorldMin.X)
            && ((boundCircle.CenterY) <= XNACS1Base.World.WorldMax.Y)
            && ((boundCircle.CenterY) >= XNACS1Base.World.WorldMin.Y);
        }
    }
}
