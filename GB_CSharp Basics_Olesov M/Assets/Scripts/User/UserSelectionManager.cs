using UnityEngine;
using UnityEngine.UI;

namespace BallGame
{
    public class UserSelectionManager
    {
        private GameObject _userSelectionWindow;
        private Dropdown _userListDropdown;
        private Button _selectUserButton;
        private Button _createUserButton;
        private Button _deleteUserButton;
        private Button _quitButton;
        private GameObject _createUserWindow;
        private Button _createButton;
        private Text _userNameInput;

        private UserDataHandler _usersData;

        public delegate void SelectionDone();
        public event SelectionDone BeginGame;

        public UserSelectionManager(UserDataHandler usersData)
        {
            _usersData = usersData;

            _userSelectionWindow = GameObject.Find("UserSelectionWindow");
            _userListDropdown = _userSelectionWindow.GetComponentInChildren<Dropdown>();
            _selectUserButton = GameObject.Find("SelectUserButton").GetComponent<Button>();
            _createUserButton = GameObject.Find("CreateUserButton").GetComponent<Button>();
            _deleteUserButton = GameObject.Find("DeleteUserButton").GetComponent<Button>();
            _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
            _createUserWindow = GameObject.Find("CreateUserWindow");
            _createButton = _createUserWindow.GetComponentInChildren<Button>();
            _userNameInput = GameObject.Find("UserNameText").GetComponent<Text>();

            _createUserWindow.SetActive(false);

            _quitButton.onClick.AddListener(Application.Quit);
            _createUserButton.onClick.AddListener(ShowCreateUserWindow);
            _createButton.onClick.AddListener(CreateUser);
            _deleteUserButton.onClick.AddListener(DeleteUser);
            _selectUserButton.onClick.AddListener(SelectUser);


            if (_usersData.UserCount > 0)
                FillInSelection();

            if (_userListDropdown.options.Count == 0)
            {
                _selectUserButton.interactable = false;
                _deleteUserButton.interactable = false;
            }
        }

        private void FillInSelection()
        {
            _userListDropdown.ClearOptions();
            for (int i = 0; i < _usersData.UserCount; i++)
                _userListDropdown.options.Add(new Dropdown.OptionData(_usersData[i].UserName));
            if (_userListDropdown.options.Count > 0)
            {
                _userListDropdown.value = 0;
                _userListDropdown.GetComponentInChildren<Text>().text = _usersData[0].UserName;
            }

            if (_userListDropdown.options.Count == 0)
            {
                _selectUserButton.interactable = false;
                _deleteUserButton.interactable = false;
            }
            else
            {
                _selectUserButton.interactable = true;
                _deleteUserButton.interactable = true;
            }
        }

        private void ShowCreateUserWindow()
        {
            _createUserWindow.SetActive(true);
        }

        private void CreateUser()
        {
            _usersData.AddUser(_userNameInput.text);
            _userNameInput.text = "";
            _createUserWindow.SetActive(false);
            FillInSelection();
        }

        private void DeleteUser()
        {
            _usersData.RemoveUser(_usersData[_userListDropdown.value]);
            FillInSelection();
        }

        private void SelectUser()
        {
            _usersData.SetSelectedUser(_userListDropdown.value);
            BeginGame?.Invoke();
            _userSelectionWindow.SetActive(false);
        }
    }
}
