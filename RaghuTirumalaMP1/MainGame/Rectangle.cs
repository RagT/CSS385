using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using XNACS1Lib;

namespace RaghuTirumala_NameSpace
{
    class Rectangle
    {
        XNACS1Rectangle m_Rectangle = null;

        public Rectangle()
        {

        }

        public void CreateRectangle()
        {
            m_Rectangle = new XNACS1Rectangle(new Vector2(5f, 2f), 10f, 2f);
            m_Rectangle.Color = Color.Red;
        }

        public void UpdateRectangle(float displacement)
        {   
            if (null != m_Rectangle)
            {
                m_Rectangle.CenterX += displacement;
            }
        }

        public XNACS1Rectangle GetRectangle()
        {
            return m_Rectangle;
        }
    }
}
