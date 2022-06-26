using System.Collections;
using UnityEngine;


public class DecoratorPatternTest : MonoBehaviour
{
    public Player player;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IGun gun = player.gun;

            SplitDecorator splitDecorator = new SplitDecorator(gun.Damage);
            player.gun = splitDecorator.Decorate(gun);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            IGun gun = player.gun;

            BounceDecorator bounceDecorator = new BounceDecorator(gun.Damage);
            player.gun = bounceDecorator.Decorate(gun);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            IGun gun = player.gun;

            BigDecorator bigDecorator = new BigDecorator(gun.Damage);
            player.gun = bigDecorator.Decorate(gun);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            IGun gun = player.gun;

            ExplodeDecorator explodeDecorator = new ExplodeDecorator(gun.Damage);
            player.gun = explodeDecorator.Decorate(gun);
        }
    }
}