using System.Collections;
using UnityEngine;


public class DecoratorPatternTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        IBullet someBullet = new Bullet(5);

        SplitDecorator splitDecorator = new SplitDecorator(5);
        someBullet = splitDecorator.Decorate(someBullet);

        someBullet.Hit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}