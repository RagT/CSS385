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
    class Bee
    {
        XNACS1Circle boundCircle = null;
        XNACS1Circle hero = null;
        private Vector2 mInitPos;
        private float mPeriods; // periods of sine curve in the world dimension X
        private float mFrequencyScale;
        private float mAmplitude;
        bool travelRight = true;
        protected enum State {
            Patrol,
            Confused
        }

        private State currentState;
        Random rand;
        private const float SPEED = 0.5f;

        public Bee(XNACS1Circle hero)
        {
            //Starting location of Bee in random position
            rand = new Random();
            float xLoc = (float) rand.NextDouble() * XNACS1Base.World.WorldDimension.X;
            float yLoc = (float)rand.NextDouble() * XNACS1Base.World.WorldDimension.Y;
            mInitPos = new Vector2(xLoc, yLoc);
            boundCircle = new XNACS1Circle(mInitPos, 1.5f);
            this.hero = hero;

            //Should be drawn above other objects
            boundCircle.TopOfAutoDrawSet();

            currentState = State.Patrol;
            SetSineCurve();
        }

        public void Update()
        {
            switch(currentState)
            {
                case State.Patrol:
                    HandlePatrolState();
                    break;
                case State.Confused:
                    HandleConfusedState();
                    break;
            }
            XNACS1Base.World.ClampAtWorldBound(boundCircle);
        }

        private void HandlePatrolState()
        {
            boundCircle.ShouldTravel = false;
            Vector2 next;
            if (travelRight)
            {
                next = new Vector2(boundCircle.CenterX + SPEED, mInitPos.Y + GetYValue(boundCircle.CenterX));
            }
            else
            {
                next = new Vector2(boundCircle.CenterX - SPEED, mInitPos.Y + GetYValue(boundCircle.CenterX));
            }
            Vector2 dir = next - boundCircle.Center;
            dir.Normalize();
            boundCircle.Center += dir;
            if ((boundCircle.CenterX + boundCircle.Radius) >= XNACS1Base.World.WorldMax.X)
            {
                travelRight = false;
            }
            if ((boundCircle.CenterX - boundCircle.Radius) <= XNACS1Base.World.WorldMin.X)
            {
                travelRight = true;
            }
            if(CheckHero())
            {
                currentState = State.Confused;
            }
        }

        private void HandleConfusedState()
        {
            boundCircle.ShouldTravel = true;
            //Assign random Velocity
            Vector2 dir = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());
            Vector2 newVelocity = dir * SPEED;

            boundCircle.Velocity = newVelocity;

            if(!CheckHero())
            {
                currentState = State.Patrol;
            }
        }

        private bool CheckHero()
        {
            float dist = 5.0f;
            Vector2 dir = boundCircle.Center - hero.Center;
            return dir.Length() <= dist;
        }

        private float GetYValue(float x)
        {
            return mAmplitude * (float)(Math.Sin(x * mFrequencyScale));
        }

        private void SetSineCurve()
        {
            mAmplitude = (0.18f + (float) rand.NextDouble() * 0.04f) * XNACS1Base.World.WorldMax.Y;
            mPeriods = 4f + (float)rand.NextDouble();
            mFrequencyScale = mPeriods * 2f * (float)(Math.PI) / XNACS1Base.World.WorldDimension.X;
        }
    }
}
