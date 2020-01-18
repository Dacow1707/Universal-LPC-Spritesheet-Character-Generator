using LPC.Spritesheet.Interfaces;
using LPC.Spritesheet.ResourceManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LPC.Spritesheet.Generator
{
    public class CharacterSpriteGenerator
    {
        private List<ISpriteSheet> _spriteLibrary;

        public CharacterSpriteGenerator(IResourceManager resoureManager)
        {
            ResourceManager = resoureManager;
        }

        public IResourceManager ResourceManager { get; set; }

        public List<ISpriteSheet> SpriteLibrary
        {
            get
            {
                if (_spriteLibrary == null)
                {
                    _spriteLibrary = new List<ISpriteSheet>();

                    // todo: revisit this later to add a 'blank' option to each
                    //foreach (SpriteLayer layer in Enum.GetValues(typeof(SpriteLayer)))
                    //{
                    //    _spriteLibrary.Add(new SpriteSheet("None", "", Gender.Either, layer));
                    //}

                    _spriteLibrary.AddRange(GetSprites("body/female", SpriteLayer.Body, SearchOption.TopDirectoryOnly));
                    //_spriteLibrary.AddRange(GetSprites("body/female/nose", SpriteLayer.Nose));
                    _spriteLibrary.AddRange(GetSprites("body/female/eyes", SpriteLayer.Eyes));
                    //_spriteLibrary.AddRange(GetSprites("body/female/ears", SpriteLayer.Ears));

                    _spriteLibrary.AddRange(GetSprites("body/male", SpriteLayer.Body, SearchOption.TopDirectoryOnly));
                    //_spriteLibrary.AddRange(GetSprites("body/male/nose", SpriteLayer.Nose));
                    _spriteLibrary.AddRange(GetSprites("body/male/eyes", SpriteLayer.Eyes));
                    //_spriteLibrary.AddRange(GetSprites("body/male/ears", SpriteLayer.Ears));

                    _spriteLibrary.AddRange(GetSprites("body", SpriteLayer.Wound, SearchOption.TopDirectoryOnly, ".+blood"));

                    _spriteLibrary.AddRange(GetSprites("facial", SpriteLayer.Facial));

                    _spriteLibrary.AddRange(GetSprites("feet", SpriteLayer.Shoes));

                    _spriteLibrary.AddRange(GetSprites("legs/pants", SpriteLayer.Legs));
                    _spriteLibrary.AddRange(GetSprites("legs/skirt", SpriteLayer.Legs));

                    _spriteLibrary.AddRange(GetSprites("feet", SpriteLayer.Boots));

                    _spriteLibrary.AddRange(GetSprites("torso", SpriteLayer.Clothes, SearchOption.AllDirectories, "^((?!plate|mail|back).)*$"));

                    _spriteLibrary.AddRange(GetSprites("torso/chain", SpriteLayer.Mail));
                    _spriteLibrary.AddRange(GetSprites("torso/chain/tabard", SpriteLayer.Jacket));

                    _spriteLibrary.AddRange(GetSprites("arms", SpriteLayer.Arms));

                    _spriteLibrary.AddRange(GetSprites("shoulders", SpriteLayer.Shoulders));

                    _spriteLibrary.AddRange(GetSprites("hands/bracers", SpriteLayer.Bracers));

                    _spriteLibrary.AddRange(GetSprites("legs/armor", SpriteLayer.Greaves));

                    _spriteLibrary.AddRange(GetSprites("hands/gloves", SpriteLayer.Gloves));

                    _spriteLibrary.AddRange(GetSprites("belt", SpriteLayer.Belts));
                    _spriteLibrary.AddRange(GetSprites("belt", SpriteLayer.Buckles, SearchOption.AllDirectories, "buckles.*"));

                    _spriteLibrary.AddRange(GetSprites("accessories", SpriteLayer.Necklaces));

                    _spriteLibrary.AddRange(GetSprites("hands/bracelets", SpriteLayer.Bracelet));

                    _spriteLibrary.AddRange(GetSprites("behind_body/cape", SpriteLayer.Cape));
                    _spriteLibrary.AddRange(GetSprites("torso/back", SpriteLayer.Cape));

                    _spriteLibrary.AddRange(GetSprites("accessories/ties", SpriteLayer.Neck));

                    _spriteLibrary.AddRange(GetSprites("weapons/left hand", SpriteLayer.Shield, SearchOption.AllDirectories, "^((?!oversize).)*$"));

                    _spriteLibrary.AddRange(GetSprites("behind_body/equipment", SpriteLayer.Quiver));

                    _spriteLibrary.AddRange(GetSprites("hair/female", SpriteLayer.Hair));
                    _spriteLibrary.AddRange(GetSprites("hair/male", SpriteLayer.Hair));

                    _spriteLibrary.AddRange(GetSprites("head", SpriteLayer.Hats));

                    _spriteLibrary.AddRange(GetSprites("weapons/right hand", SpriteLayer.Weapon));
                }
                return _spriteLibrary;
            }
        }

        public bool FilterValid(string file, string filter)
        {
            return Regex.IsMatch(file, filter);
        }

        public ICharacterSprite GetRandomCharacterSprite()
        {
            var character = new CharacterSprite(RandomHelper.Random.Next(10) > 5 ? Gender.Male : Gender.Female);

            foreach (SpriteLayer layer in Enum.GetValues(typeof(SpriteLayer)))
            {
                if (layer != SpriteLayer.Body && RandomHelper.Random.Next(100) < 25)
                {
                    continue; // skip layer
                }
                var sprites = GetSprites(layer, character.Gender).ToList();
                if (sprites.Count > 0)
                {
                    character.AddLayer(sprites[RandomHelper.Random.Next(0, sprites.Count)]);
                }
            }

            return character;
        }

        public IEnumerable<ISpriteSheet> GetSprites(SpriteLayer layer, Gender gender)
        {
            return SpriteLibrary.Where(s => s.SpriteLayer == layer && (s.Gender == gender || s.Gender == Gender.Either));
        }

        private Gender GetGender(string fileName)
        {
            if (fileName.ToLower().Contains("female") || fileName.ToLower().Contains("woman"))
            {
                return Gender.Female;
            }
            if (fileName.ToLower().Contains("male") || fileName.ToLower().Contains("man"))
            {
                return Gender.Male;
            }

            return Gender.Either;
        }

        private List<ISpriteSheet> GetSprites(string path, SpriteLayer layer, SearchOption option = SearchOption.AllDirectories, string filterRegex = ".*")
        {
            var sheets = new List<ISpriteSheet>();

            var files = ResourceManager.GetSprites(path, option);

            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);

                if (FilterValid(name, filterRegex))
                {
                    sheets.Add(new SpriteSheet(name, ResourceManager.GetImageStream(file), GetGender(file), layer));
                }
            }

            return sheets;
        }
    }
}