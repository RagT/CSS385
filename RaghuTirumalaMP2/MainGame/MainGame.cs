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
        StableWall m_Wall = null;
        StableWall m_Wall2 = null;
        SmartWall m_Smart = null;
        SmartWall m_Smart2 = null;
        public MainGame()
        {
        }

        protected override void InitializeWorld()
        {
            World.SetWorldCoordinate(new Vector2(0, 0), 100);
            m_Hero = new Hero((World.WorldMax + World.WorldMin) / 2);
            SetAppWindowPixelDimension(false, 700, 500);
            m_Wall = new StableWall(new Vector2(50, World.WorldMax.Y * 0.8f), 50, 3);
            m_Wall2 = new StableWall(new Vector2(50, World.WorldMax.Y * 0.1f), 50, 3);
            m_Smart = new SmartWall(new Vector2(20, World.WorldMax.Y * 0.5f), 3, World.WorldMax.Y * 0.5f);
            m_Smart2 = new SmartWall(new Vector2(70, World.WorldMax.Y * 0.5f), 3, World.WorldMax.Y * 0.5f);

            m_Bee = new Bee(m_Hero.GetCircle());
            World.SetBackgroundTexture("water");
            PlayBackgroundAudio("bg", 0.5f);
        }

        
        protected override void UpdateWorld()
        {
            if (GamePad.Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Object update calls
            m_Hero.Update();
            m_Bee.Update();
            m_Wall.Update(m_Hero.GetCircle());
            m_Wall2.Update(m_Hero.GetCircle());
            m_Smart.Update(m_Hero.GetCircle(), m_Bee.IsChaser());
            m_Smart2.Update(m_Hero.GetCircle(), m_Bee.IsChaser());
        }

    }
}
