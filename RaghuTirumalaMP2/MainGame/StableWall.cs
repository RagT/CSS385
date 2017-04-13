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
    class StableWall
    {
        public XNACS1Rectangle boundRect = null;

        public StableWall(Vector2 location, float width, float height)
        {
            boundRect = new XNACS1Rectangle(location, width, height);
            boundRect.Texture = "stopWall";
        }

        public void Update(XNACS1Circle hero)
        {
            CollisionWithHero(hero);
            XNACS1Base.World.ClampAtWorldBound(boundRect);
        }

        //Handles Collision with the hero
        public void CollisionWithHero(XNACS1Circle hero)
        {
            float xDiff = hero.CenterX - boundRect.CenterX;
            float yDiff = hero.CenterY - boundRect.CenterY;
            float radius = hero.Radius;

            //Check for collision first
            if( (Math.Abs(xDiff) - radius) <= (boundRect.Width / 2) 
               && (Math.Abs(yDiff) - radius) <= (boundRect.Height / 2))
            {
                //Handle Interpenetration
                if((Math.Abs(xDiff) - boundRect.Width / 2) < (Math.Abs(yDiff) - boundRect.Height / 2))
                {
                    //Move hero in y direction 
                    if (yDiff < 0)
                    {
                        float distToMove = yDiff + (boundRect.Height / 2); 
                        hero.CenterY += distToMove;
                    }
                    else
                    {
                        float distToMove = yDiff - (boundRect.Height / 2);
                        hero.CenterY += distToMove;
                    }
                }
                else
                {
                    //Move Hero in X Direction
                    if(xDiff < 0)
                    {
                        float distToMove = xDiff + (boundRect.Width / 2);
                        hero.CenterX += distToMove;
                    }
                    else
                    {
                        float distToMove = xDiff - (boundRect.Width / 2);
                        hero.CenterX += distToMove;
                    }
                }
                
            } 
            
        }
    }
}
