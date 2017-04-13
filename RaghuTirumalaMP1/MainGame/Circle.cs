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
    class Circle
    {
        XNACS1Circle m_Circle = null;
        Vector2 m_Velocity = new Vector2(0f, 0f);
        private static readonly Random random = new Random();

        public Circle()
        {
   
        }

        public void CreateCircle(Vector2 pos)
        {
            m_Circle = new XNACS1Circle(pos, 2f, "SoccerBall");
            m_Velocity.X = (float) randInBetween(0.5, 2);
            m_Velocity.Y = (float)randInBetween(0.5, 2);
            m_Circle.Velocity = m_Velocity;
            m_Circle.ShouldTravel = true;
        }

        public void DestroyCircle()
        {
            if (m_Circle != null)
            {
                m_Circle.RemoveFromAutoDrawSet();
                m_Circle = null;
            }
        }

        public XNACS1Circle GetCircle()
        {
            return m_Circle;
        }
        
        public void FlipVelocity(bool x)
        {
            if(x)
            {
                m_Velocity.X = -1 * m_Velocity.X;
            } 
            else
            {
                m_Velocity.Y = -1 * m_Velocity.Y;
            }
            m_Circle.Velocity = m_Velocity;
        }

        //Simple collision detection algorithm. No interpenetration resolution
        public bool rectangleCollision(XNACS1Rectangle rect)
        {
            if(m_Circle == null || rect == null)
            {
                return false;
            }          
            Vector2 c_center = m_Circle.Center;
            Vector2 r_center = rect.Center;

            Vector2 cToR = r_center - c_center;
            cToR = Vector2.Normalize(cToR);
            cToR = Vector2.Multiply(cToR, m_Circle.Radius);

            Vector2 intPoint = c_center + cToR;

            if(intPoint.X <= rect.MaxBound.X && intPoint.X >= rect.MinBound.X
                && intPoint.Y <= rect.MaxBound.Y && intPoint.Y >= rect.MinBound.Y)
            {
                m_Circle.CenterY += 1;
                FlipVelocity(false);
                return true;
            }
            return false;
        }

        private double randInBetween(double min, double max)
        {
            return min + (random.NextDouble() * (max - min));
        }
    }
}
