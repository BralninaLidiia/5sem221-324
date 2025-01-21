using System;
using System.Drawing;
using System.Windows;



namespace MinesweeperLib
{
    public class Minesweeper
    {
        private int _areaSize;
        private int _gamemod;
        private int _fightersCount;
        private int _fightersFirstArmy = -1;
        private int _fightersSecondArmy = -1;
        private int[] _firstArmy;
        private int[] _secondArmy;
        private int _playerNumber;
        private int _roundCounter = 1; // Задаём значение по умолчанию
        private string _firstPlayerPicturePath;
        private string _secondPlayerPicturePath;
        private static Random _random = new Random();
        public event EventHandler<ArmyUpdatedEventArgs> ArmyUpdated;
        public int AreaSize
        {
            get { return _areaSize; }
            set { _areaSize = value; } // Можно добавить проверки при необходимости
        }
        public int PayerNumber
        {
            get { return _playerNumber; }
            set { _playerNumber = value; } // Можно добавить проверки при необходимости
        }

        public int Gamemod
        {
            get { return _gamemod; }
            set { _gamemod = value; }
        }

        public int FightersCount
        {
            get { return _fightersCount; }
            set { _fightersCount = value; }
        }

        public int FightersFirstArmy
        {
            get { return _fightersFirstArmy; }
            set
            {
                if (value >= 0)  // Пример проверки для положительного значения
                {
                    _fightersFirstArmy = value;
                }
                else
                {
                    // Обработка ошибки (например, выбросить исключение)
                    throw new ArgumentOutOfRangeException("FightersFirstArmy", "Значение должно быть больше или равно 0");
                }

            }
        }


        public int FightersSecondArmy
        {
            get { return _fightersSecondArmy; }
            set
            {
                if (value >= 0)
                {
                    _fightersSecondArmy = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("FightersSecondArmy", "Значение должно быть больше или равно 0");
                }
            }
        }

        public int[] FirstArmy
        {
            get { return _firstArmy; }
            set { _firstArmy = value; }
        }

        public int[] SecondArmy
        {
            get { return _secondArmy; }
            set { _secondArmy = value; }
        }

        public int RoundCounter
        {
            get { return _roundCounter; }
            set { _roundCounter = value; }
        }

        public string FirstPlayerPicturePath
        {
            get { return _firstPlayerPicturePath; }
            set { _firstPlayerPicturePath = value; }
        }

        public string SecondPlayerPicturePath
        {
            get { return _secondPlayerPicturePath; }
            set { _secondPlayerPicturePath = value; }
        }
        
        private void countArmy(int _areaSize)
        {
            switch (_areaSize)
            {
                case 3:
                    _fightersCount = 2;
                    break;
                case 4:
                    _fightersCount = 4;
                    break;
                case 5:
                    _fightersCount = 6;
                    break;
                case 6:
                    _fightersCount = 8;
                    break;
            }
            if (_fightersFirstArmy == -1 && _fightersSecondArmy == -1)
            {
                _fightersFirstArmy = _fightersCount;
                _fightersSecondArmy = _fightersCount;
            }

        }
        public void ComputerPlayer()
        {
            if (_roundCounter % 2 == 1)
            {
                int counter = 1;
                for (int i = 1; counter <= _fightersSecondArmy; i++)
                {
                    int index = _random.Next(0, _secondArmy.Length);
                    if (_secondArmy[index] == 0)
                    {
                        _secondArmy[index] = counter;
                        counter++;
                    }
                }
            }
            else
            {
                int counter = 1;
                for (int i = 1; counter <= _fightersFirstArmy; i++)
                {
                    int index = _random.Next(0, _firstArmy.Length);
                    if (_firstArmy[index] == 0)
                    {
                        _firstArmy[index] = counter;
                        counter++;
                    }
                }
            }
            
        }


        public void Swap()
        {
            int[] saverArmy = _secondArmy;
            _secondArmy = _firstArmy;
            _firstArmy = saverArmy;
            int saverCount = _fightersSecondArmy;
            _fightersSecondArmy = _fightersFirstArmy;
            _fightersFirstArmy = saverCount;

        }
        public void StartGame()
        {
            countArmy(_areaSize);
            _firstArmy = new int[_areaSize*_areaSize];
            _secondArmy = new int[_areaSize * _areaSize];
            _roundCounter = 1;
        }
        private int GetTotalUnitsFirstPlayer()
        {
            if (_firstArmy == null) return 0;
            int count = 0;
            for (int i = 0; i < _firstArmy.Length; i++)
            {
                count += _firstArmy[i];
            }
            return count;
        }

        private int GetTotalUnitsSecondPlayer()
        {
            if (_secondArmy == null) return 0;
            int count = 0;
            for (int i = 0; i < _secondArmy.Length; i++)
            {
                count += _secondArmy[i];
            }
            return count;
        }

        public bool AddUnitFirstArmy(int index)
        {
            if (_firstArmy == null) return false;
            if (index < 0 || index >= _firstArmy.Length) return false; // <= исправил
            if (GetTotalUnitsFirstPlayer() < _fightersFirstArmy)
            {
                _firstArmy[index] = 1;
                return true;
            }
            return false;
        }

        public bool AddUnitSecondArmy(int index)
        {
            if (_secondArmy == null) return false;
            if (index < 0 || index >= _secondArmy.Length) return false; // <= исправил
            if (GetTotalUnitsSecondPlayer() < _fightersSecondArmy)
            {
                _secondArmy[index] = 1;
                return true;
            }
            return false;
        }
        public int GetNextClickNumber(int armyNumber)
        {
            if (armyNumber == 1)
            {
                return _firstArmy.Where(x => x > 0).Count() + 1;
            }
            else
            {
                return _secondArmy.Where(x => x > 0).Count() + 1;
            }
        }

        public void UpdateArmyArray(int armyNumber, int? changedClickNumber = null)
        {
            if (armyNumber == 1)
            {
                if (changedClickNumber.HasValue)
                {
                    for (int i = 0; i < _firstArmy.Length; i++)
                    {
                        if (_firstArmy[i] > changedClickNumber)
                        {
                            _firstArmy[i]--;
                        }
                    }
                }
                OnArmyUpdated(1, string.Join(", ", _firstArmy));

            }
            else
            {
                if (changedClickNumber.HasValue)
                {
                    for (int i = 0; i < _secondArmy.Length; i++)
                    {
                        if (_secondArmy[i] > changedClickNumber)
                        {
                            _secondArmy[i]--;
                        }
                    }
                }
                OnArmyUpdated(2, string.Join(", ", _secondArmy));
            }
        }

        protected virtual void OnArmyUpdated(int armyNumber, string arrayContent)
        {
            ArmyUpdated?.Invoke(this, new ArmyUpdatedEventArgs(armyNumber, arrayContent));
        }

        
        public bool Check(int i)
        {
            if (_firstArmy[i] != 0 && _secondArmy[i] != 0)
            {
                if (_secondArmy[i] >0)
                    _fightersSecondArmy -= 1;
                else
                    _fightersFirstArmy -= 1;
                _secondArmy[i] = 0;
                _firstArmy[i] = 0;
                return true;
            }
            else if (_firstArmy[i] > 0 && _secondArmy[i] == 0)
            {
                _firstArmy[i] = -1;
            }
            else if (_firstArmy[i] == 0 && _secondArmy[i] > 0)
            {
                _secondArmy[i] = -1;
            }
            return false;
        }
        public void UpdateSecondArmy(int i)
        {
            if (_secondArmy[i] > 0)
            {
                _secondArmy[i] = -1;
            }
        }
    }

    public class ArmyUpdatedEventArgs : EventArgs
    {
        public int ArmyNumber { get; }
        public string ArrayContent { get; }

        public ArmyUpdatedEventArgs(int armyNumber, string arrayContent)
        {
            ArmyNumber = armyNumber;
            ArrayContent = arrayContent;
        }
    }
}
