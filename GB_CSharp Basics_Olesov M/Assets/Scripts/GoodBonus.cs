using UnityEngine;

namespace BallGame
{
    public class GoodBonus : InteractiveObject, ILevitateable, IFlickerable
    {
        protected Material _material;
        private float _levitationHeight;

        protected void Start()
        {
            _material = GetComponent<Renderer>().material;
            _levitationHeight = Random.Range(1.0f, 2.0f);
            _material.color = Color.blue;
        }

        protected override void Interaction()
        {

        }

        public override void Execute()
        {
            if (!IsInteractable) { return; }
            Levitate();
            Flicker();
        }
        public void Flicker()
        {
            _material.color = new Color(_material.color.r, _material.color.g,
                                        _material.color.b, Mathf.PingPong(Time.time, 1.0f));
        }

        public void Levitate()
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                                                Mathf.PingPong(Time.time, _levitationHeight),
                                                transform.localPosition.z);
        }
    }
}

