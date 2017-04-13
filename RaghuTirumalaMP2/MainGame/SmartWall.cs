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
    class SmartWall : StableWall
    {
        protected enum State
        {
            Ambient,
            Angry,
            HasChaser
        }

        State currentState;
        int ticks = 40;
        bool right = true;

        public SmartWall(Vector2 location, float width, float height) : base(location, width, height)
        {
            currentState = State.Ambient;
        }

        public void Update(XNACS1Circle hero, bool chaser)
        {
            switch(currentState)
            {
                case State.Ambient:
                    HandleAmbient(hero, chaser);
                    break;
                case State.Angry:
                    HandleAngry(hero, chaser);
                    break;
                case State.HasChaser:
                    HandleHasChaser(chaser);
                    break;
            }
            CollisionWithHero(hero);
            ticks--;
        }        

        private void HandleAmbient(XNACS1Circle hero, bool chaser)
        {
            boundRect.Texture = "ambientWall";
            WallMovement(0.125f);
            if (CheckProtectionDistance(hero))
            {
                currentState = State.Angry;
            }
            if (chaser)
            {
                currentState = State.HasChaser;
            }
        }

        private void HandleAngry(XNACS1Circle hero, bool chaser)
        {
            boundRect.Texture = "angryWall";
            WallMovement(0.5f);
            if(!CheckProtectionDistance(hero))
            {
                currentState = State.Ambient;
            }
            if(chaser)
            {
                currentState = State.HasChaser;
            }
        }

        private void HandleHasChaser(bool chaser)
        {
            boundRect.Texture = "stopWall";
            if(!chaser)
            {
                currentState = State.Ambient;
            }
        }

        private void WallMovement(float dist)
        {
            if (ticks > 0)
            {
                if (right)
                {
                    boundRect.CenterX += dist;
                }
                else
                {
                    boundRect.CenterX -= dist;
                }
            }
            else
            {
                right = !right;
                ticks = 40;
            }
        }

        //Return true if hero is violating walls protection distance
        private bool CheckProtectionDistance(XNACS1Circle hero)
        {
            float ProtectionDist = 0;
            if(boundRect.Width > boundRect.Height)
            {
                ProtectionDist = 0.7f * boundRect.Width;
            }
            else
            {
                ProtectionDist = 0.7f * boundRect.Height;
            }
            Vector2 toWall = boundRect.Center - hero.Center;
            return toWall.Length() <= ProtectionDist;
        }
    }
}
