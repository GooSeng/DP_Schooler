using Schooler.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Pages
{
    public class LoginPage : ContentPage
    {
        Entry id;
        Entry pw;
        public LoginPage()
        {
            UserDao dao = new UserDao();
      
            var loginBtn = new Button() {
                Text = "Sign In"
            };
            loginBtn.Clicked += LoginBtn_Clicked;

            var joinBtn = new Button() {
                Text = "Sign up"
            };
            joinBtn.Clicked += joinBtn_Clicked;
            
            id = new Entry()
            {
                Placeholder = "ID"
            };
            pw = new Entry()
            {
                Placeholder = "Password",
                IsPassword = true
            };

            Title = "Schooler";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,

                Children = {
                    new Label {
                        Text = "Login Page",
                        HorizontalOptions = LayoutOptions.Center,
                   },
                    id,
                    pw,
                    loginBtn,
                    joinBtn
                }
            };

            Padding = new Thickness(10, 10, 10, 10);
            
        }

        private void joinBtn_Clicked(object sender, EventArgs e)
        {
            UserDao dao = new UserDao();

            if(dao.CheckUser(id.Text))
            {
                DisplayAlert("회원가입 실패", "이미 존재하는 아이디 입니다", "확인");
                return;
            }
           

            dao.SignUp(id.Text, pw.Text);
            DisplayAlert("회원가입 성공", "해당 회원으로 로그인 합니다.", "확인");
            LoginBtn_Clicked(null, null);

        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {
            // 로긴처리
            //App.Current.MainPage = new MainPage();
            UserDao dao = new UserDao();
            bool isLogined = dao.SignIn(id.Text, pw.Text);


            if (isLogined)
                this.Navigation.PushAsync(new MainPage());
            else
				DisplayAlert("fail", "no info", "ok");
//			DisplayAlert("로그인 실패", "존재하지 않는 아이디 혹은 틀린 비밀번호 입니다", "확인");

        }
    }
}
