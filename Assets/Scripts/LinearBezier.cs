using UnityEngine;

public class LinearBezier {

	Vector2 p1;
	Vector2 p2;

	public LinearBezier(Vector2 p1, Vector2 p2)
	{
		this.p1 = p1;
		this.p2 = p2;
	}

	public Vector2 getPoint(float t)
	{
		if (t < 0 || t > 1) return new Vector2(0, 0);

		Vector2 toReturn = new Vector2();

		toReturn.x = (1-t) * p1.x + t * p2.x;
		toReturn.y = (1-t) * p1.y + t * p2.y;

		return toReturn;
	}
}
