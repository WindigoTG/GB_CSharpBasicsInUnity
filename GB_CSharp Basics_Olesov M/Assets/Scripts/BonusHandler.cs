//����� ��� ���������� ������������.

namespace BallGame
{
    public class BonusHandler
    {
        private GoodBonus[] _goodBonus; //������ ������������� �������
        private BadBonus[] _badBonus; //������ ������������� �������

        public int goodBonusLength { get; private set; } //������������, ���������� ����� ������� ������������� �������
        public int badBonusLength { get; private set; } //������������, ���������� ����� ������� ������������� �������

        public BonusHandler(GoodBonus[] goodBonus, BadBonus[] badBonus)
        {
            _goodBonus = goodBonus;
            goodBonusLength = _goodBonus.Length;
            _badBonus = badBonus;
            badBonusLength = _badBonus.Length;
        }

        //���������� ��� ������� � ������� ������������� �������
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

        //������������� ���������� ��� ������� � ������� ������������� �������
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