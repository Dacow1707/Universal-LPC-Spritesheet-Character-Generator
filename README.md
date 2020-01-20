Universal LPC Spritesheet Character Generator
=============================================

Based on the Universal Spritesheet generator by [sanderfrenken](https://sanderfrenken.github.io/Universal-LPC-Spritesheet-Character-Generator/) which in turn was based on was based on [Universal LPC Spritesheet](https://github.com/jrconway3/Universal-LPC-spritesheet).

The project you are looking now is an expansion on the above mentioned projects. I try to include all LPC created art up to now.

The Liberated Pixel Effort is a collaborative effort from a number of different great artists who helped produce sprites for the project.
Please read the [authors](AUTHORS.txt) file for the full list of authors who have contributed to the spritesheet.

If you want to know how to include sprites from this sheet into your work, please visit the [Open Game Art LPC forums](http://opengameart.org/forums/liberated-pixel-cup).
You will need to credit everyone who helped contribute to the LPC sprites you intend to use if you wish to use LPC sprites in your project.

### Licensing

According to the rules of the LPC all art submissions were dual licensed under both GNU GPL 3.0 and CC-BY-SA 3.0. These art submissions are considered all images present in the directory `spritesheets` and it's subdirectories. Further work produced in this repository is licensed under the same terms.

CC-BY-SA 3.0:
 - http://creativecommons.org/licenses/by-sa/3.0/
 - See the file: [cc-by-sa-3.0](cc-by-sa-3_0.txt)

GNU GPL 3.0:
 - http://www.gnu.org/licenses/gpl-3.0.html
 - See the file: [gpl-3.0](gpl-3_0.txt)

### Run

Add the two assemblies LPC.Spritesheet.Generator.dll and LPC.Spritesheet.ResourceManager.dll to your Unity project (Tested with Unity 2019.3.0f5).

Create a 2D Sprite object and attach this script:

```c#
    public class SpriteTester : MonoBehaviour
    {
        // the attached renderer
        private SpriteRenderer _renderer;

        // objec to hold the sheet definition
        private CharacterSpriteSheet _characterSpriteSheet;

        // keep track of the current animation frame
        private int _frame;

        // keep track of time to only update the frame every second
        private float _delta;

        private void Start()
        {
            // get the attached renderer
            _renderer = GetComponent<SpriteRenderer>();

            // create a generator (this loads everything in the resource manager into memory, so if you need a few of these keep this as a singleton somewhere)
            var generator = new CharacterSpriteGenerator(new EmbeddedResourceManager());

            // generate the sprite, this will go over and select random items and all 27 layers and merge them into a single texture (expensive, takes ~200ms)
            _characterSpriteSheet = new CharacterSpriteSheet(generator.GetRandomCharacterSprite());
        }

        private void Update()
        {
            _delta += Time.deltaTime;

            if (_delta > 1f)
            {
                _delta = 0;
                // send in a refrence to the frame integer, will increment and go over the items (if it goes over it will reset to 0 to loop the animation)
                _renderer.sprite = _characterSpriteSheet.GetFrame(Animation.Walk, Orientation.Front, ref _frame);
            }
        }
    }
```

### Caution

Creating sprites are resource intensive and will take several frames to continue, plan accordingly or your game will hitch every time you run.

### Status

The Unity integration is mostly done, I am using it in my game and will be fixing bugs as I find them.

### Examples

![alt text](https://github.com/sanderfrenken/Universal-LPC-Spritesheet-Character-Generator/blob/master/Example.mp4)
