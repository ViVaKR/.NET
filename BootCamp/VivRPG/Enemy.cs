namespace VivRPG;

public class Enemy
{
    // 속도 (velocity)
    // float velocityX;
    // float velocityY;
    // float velocityZ;
    // float targetPositionX;
    // float targetPositionY;
    // float targetPositionZ;

    Vector3 velocity = new Vector3(3, 4, 5);
    Vector3 targetPosition = new(0, 9, 4);

    void SemoMethod() {
        velocity.x = 5;
        float speed = velocity.magnitude;

        Vector3 direction = velocity.Normalized;
    }
}

//? 유니티에 내장된 기능
// Vector * scalar;
// => (5, 10, 15) * 2 = (10, 20, 30)

// Vector / scalar;
// => (5, 10, 15) / 5 = (1, 2, 3)

// Vector + Vector;
// => (5, 10, 15) + (3, 4, 5)= (8, 14, 20)

// Vector - Vector;
// => (5, 10, 15) + (3, 4, 5)= (2, 6, 10)
