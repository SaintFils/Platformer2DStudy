using UnityEngine;

namespace Code.Utils
{
    public class ContactPooler
    {
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];

        private int _contactCount;
        private Collider2D _collider2D;

        public bool IsGrounded { get; private set; }
        public bool HasLeftContact { get; private set; }
        public bool HasRightContact { get; private set; }

        public ContactPooler(Collider2D collider)
        {
            _collider2D = collider;
        }

        public void Tick()
        {
            IsGrounded = false;
            HasLeftContact = false;
            HasRightContact = false;

            _contactCount = _collider2D.GetContacts(_contacts);

            for (int i = 0; i < _contactCount; i++)
            {
                if (_contacts[i].normal.y > Constants.Constants.FloatConstants.CollisionThreshold) IsGrounded = true;
                if (_contacts[i].normal.x > Constants.Constants.FloatConstants.CollisionThreshold) HasLeftContact = true;
                if (_contacts[i].normal.x > -Constants.Constants.FloatConstants.CollisionThreshold) HasRightContact = true;
                //todo check if in HasRightContact should be normal.y
            }
        }
    }
}
