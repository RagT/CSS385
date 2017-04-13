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

namespace MainGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : XNACS1Base
    {
        Hero m_Hero = null;
        Bee m_Bee = null;

        public MainGame()
        {
        }

        protected override void InitializeWorld()
        {
            m_Hero = new Hero((World.WorldMax + World.WorldMin) / 2);
            SetAppWindowPixelDimension(false, 700, 500);
            m_Bee = new Bee(m_Hero.GetCircle());
            World.SetBackgroundTexture("water");
        }

        
        protected override void UpdateWorld()
        {
            if (GamePad.Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Object update calls
            m_Hero.Update();
            m_Bee.Update();
        }

    }
}
