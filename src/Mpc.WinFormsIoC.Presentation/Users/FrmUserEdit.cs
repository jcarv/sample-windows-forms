using Mpc.WinFormsIoC.Application.Dto;
using Mpc.WinFormsIoC.Application.Services.Countries;
using Mpc.WinFormsIoC.Application.Services.Users;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mpc.WinFormsIoC.Presentation.Users
{
    public partial class FrmUserEdit : Form
    {
        private IUserService _userService;
        private ICountryService _countryService;

        public FrmUserEdit(IUserService userService, ICountryService countryService)
        {
            InitializeComponent();
            _userService = userService;
            _countryService = countryService;
        }

        public int? UserId { get; set; } = null;

        private async void BtnSave_ClickAsync(object sender, System.EventArgs e)
        {
            if (UserId.HasValue)
            {
                await UpdateUserAsync();
                Core.Messages.Information.ShowMessage("User updated", "Users");
            }
            else
            {
                await SaveUserAsync();
                Core.Messages.Information.ShowMessage("User created", "Users");
            }
            Close();
        }

        private void FillUser(UserDto user)
        {
            TxtId.Text = user.Id.ToString();
            TxtEmail.Text = user.Email;
            TxtName.Text = user.Name;
            TxtPassword.Text = user.Password;
            CmbCountry.SelectedValue = (user.CountryID == null ? -1 : user.CountryID);
        }

        private async void FrmUserEdit_Load(object sender, System.EventArgs e)
        {
            if (!UserId.HasValue)
            {
                return;
            }

            using (new Core.ShowLoading())
            {
                var existUser = await _userService.FindAsync(UserId.Value);

                if (existUser != null)
                {
                    var emptyCountry = new CountryDto();
                    emptyCountry.Id = -1;

                    var countries = await _countryService.GetAllAsync();
                    countries.Insert(0, emptyCountry);

                    countryDtoBindingSource.DataSource = countries;
                    countryDtoBindingSource.ResetBindings(false);

                    FillUser(existUser);
                }
            }
        }

        private UserDto GetUser()
        {
            var selectedCountryID = (int)CmbCountry.SelectedValue;
            var user = new UserDto
            {
                Email = TxtEmail.Text,
                Name = TxtName.Text,
                Password = TxtPassword.Text,
                Username = TxtUsername.Text,
                CountryID = (selectedCountryID == -1 ? (int?)null : selectedCountryID),
                //if (CmbCountry.SelectedValue != -1) { CountryID = (int)CmbCountry.SelectedValue },
            };

            return user;
        }

        private async Task SaveUserAsync()
        {
            var user = GetUser();

            await _userService.CreateAsync(user);
        }

        private async Task UpdateUserAsync()
        {
            var user = GetUser();
            user.Id = UserId.Value;
            user.Password = string.Empty;

            await _userService.UpdateAsync(user);
        }
    }
}
