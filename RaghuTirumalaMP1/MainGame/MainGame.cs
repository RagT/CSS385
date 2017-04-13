#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

using XNACS1Lib;

namespace RaghuTirumala_NameSpace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame: XNACS1Base
    {
        Rectangle paddle = null;
        Circle ball = null;
        String collisionStatus;
        int bounces = 0;

        public MainGame()
        {
        }

        protected override void InitializeWorld()
        {
            World.SetWorldCoordinate(new Vector2(0f, 0f), 100f);
            World.SetBackgroundTexture("water");
            SetAppWindowPixelDimension(false, 600, 500);
            paddle = new Rectangle();
            paddle.CreateRectangle();
            ball = new Circle();
            PlayBackgroundAudio("bg", 0.5f);
            collisionStatus = " ";
        }

        
        protected override void UpdateWorld()
        {
            //Exit when back button or escape key pressed
            if (GamePad.Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Spawn a ball
            if(GamePad.ButtonAClicked())
            {
                ball.DestroyCircle();
                ball.CreateCircle((World.WorldMin + World.WorldMax) / 2f);
                bounces = 0;
            }

            //Move paddle with right thumbstick
            paddle.UpdateRectangle(GamePad.ThumbSticks.Right.X * 2);
            World.ClampAtWorldBound(paddle.GetRectangle());

            //Check for ball collision
            XNACS1Circle ballCirc = ball.GetCircle();
            if(ballCirc == null)
            {
                return;
            }
            Vector2 ballCenter = ballCirc.Center;
            float maxX = World.WorldMax.X;
            float maxY = World.WorldMax.Y;
            float minX = World.WorldMin.X;
            float minY = World.WorldMin.Y;
            float rad = ballCirc.Radius;

            //Right boundary
            if(ballCenter.X > (maxX - rad))
            {
                collisionStatus = "Bounce Right";
                ball.FlipVelocity(true);
                PlayACue("Bounce");
            }

            //Left boundary
            if(ballCenter.X < (minX + rad))
            {
                collisionStatus = "Bounce Left";
                ball.FlipVelocity(true);
                PlayACue("Bounce");
            }

            //Top boundary
            if (ballCenter.Y > maxY)
            {
                collisionStatus = "Bounce Top";
                ball.FlipVelocity(false);
                PlayACue("Bounce");
            }

            //Bottom Boundary
            if (ballCenter.Y < minY)
            {
                PlayACue("die");
                //ball leaving screen; reset bounces and get rid of circle
                bounces = 0;
                ball.DestroyCircle();
                collisionStatus = " ";
            }

            //paddle collision handling
            if (ball.rectangleCollision(paddle.GetRectangle()))
            {
                bounces++;
                PlayACue("powerup");
            }
           
            //Status Messages
            EchoToTopStatus("Latest Collison: " + collisionStatus + "   Bounces: " + bounces);
        }

    }
}
