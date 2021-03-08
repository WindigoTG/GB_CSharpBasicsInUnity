// ласс дл€ реализации индексаторов.

namespace BallGame
{
    public class BonusHandler
    {
        private GoodBonus[] _goodBonus; //ћассив положительных бонусов
        private BadBonus[] _badBonus; //ћассив отрицательных бонусов

        public int goodBonusLength { get; private set; } //јвтосвойство, содержащее длину массива положительных бонусов
        public int badBonusLength { get; private set; } //јвтосвойство, содержащее длину массива отрицательных бонусов

        public BonusHandler(GoodBonus[] goodBonus, BadBonus[] badBonus)
        {
            _goodBonus = goodBonus;
            goodBonusLength = _goodBonus.Length;
            _badBonus = badBonus;
            badBonusLength = _badBonus.Length;
        }

        //»ндексатор дл€ доступа к массиву положительных бонусов
        public GoodBonus this[int index]
        {
            get
            {
                if (index >= 0 && index < goodBonusLength)
                    return _goodBonus[index];
                else
                    return null;
            }

            set
            {
                if (index >= 0 && index < goodBonusLength)
                    _goodBonus[index] = value;
            }
        }

        //ѕерегруженный индексатор дл€ доступа к массиву отрицательных бонусов
        public BadBonus this[double ind]
        {
        get
            {
                int index;
                if ((ind - (int)ind) < 0.5) index = (int)ind;
                else index = (int)ind + 1;

                if (index >= 0 && index < badBonusLength)
                    return _badBonus[index];
                else
                    return null;
            }

            set
            {
                int index;
                if ((ind - (int)ind) < 0.5) index = (int)ind;
                else index = (int)ind + 1;

                if (index >= 0 && index < badBonusLength)
                    _badBonus[index] = value;
            }
        }
    }
}