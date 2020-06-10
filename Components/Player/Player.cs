﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zen;
using Zen.Components;

namespace Nihhus.Components
{
    public class Player : Component, IUpdatable
    {
        public Transform Transform;
        public Vector2 Move { get; set; }
        public SpriteAnimator Animator;

        public Mover Mover;

        //public BoxCollider VesselCatchCollider;
        /*public Vector2 VesselColliderBounds;
        public Rectangle TextureBounds;*/
        public bool ControlsEnabled = true;

        int _lastMoveX;

        public Vector2 FacingDirection
        {
            get
            {
                if (Move == Vector2.Zero)
                    return new Vector2(_lastMoveX, 0);
                else if (Move.X == 0)
                    return new Vector2(_lastMoveX, Move.Y);
                else
                    return Move;
            }
        }

        StateMachine<Player> _stateMachine;
        State<Player> _idleState = new PlayerIdleState();
        State<Player> _swimState = new PlayerSwimState();
        State<Player> _catchState = new PlayerCatchState();

        public override void Mount()
        {   
            Mover = GetComponent<Mover>();
            Animator = GetComponent<SpriteAnimator>();
            Transform = GetComponent<Transform>();
            
            _stateMachine = new StateMachine<Player>(this, _idleState);
            _stateMachine.AddState(_swimState);
            _stateMachine.AddState(_catchState);
            
            Transform.Position = Screen.Center;
            /*VesselColliderBounds = new Vector2(31, 2);
            VesselCatchCollider = new BoxCollider(VesselColliderBounds.X, VesselColliderBounds.Y);
            VesselCatchCollider.IsTrigger = true;
            AddComponent(VesselCatchCollider);
            VesselCatchCollider.Enabled = false;*/
        }

        public enum Controls
        {
            Catch = Keys.J,
            Up = Keys.W,
            Down = Keys.S,
            Left = Keys.A,
            Right = Keys.D
        }

        public void Update()
        {
            if (ControlsEnabled)
            {
                KeyboardState kb = Keyboard.GetState();

                int moveX = Convert.ToInt32(kb.IsKeyDown((Keys) Controls.Right)) -
                            Convert.ToInt32(kb.IsKeyDown((Keys) Controls.Left));
                int moveY = Convert.ToInt32(kb.IsKeyDown((Keys) Controls.Down)) -
                            Convert.ToInt32(kb.IsKeyDown((Keys) Controls.Up));

                Move = new Vector2(moveX, moveY);

                if (Move.X != 0)
                _lastMoveX = (int) Move.X;

                Transform.FlipX = FacingDirection.X == -1;

                if (kb.IsKeyDown((Keys) Controls.Catch))
                    _stateMachine.ChangeState<PlayerCatchState>();
            }
            
            _stateMachine.Update(Zen.Time.DeltaTime);
        }
    }
}