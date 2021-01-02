define([], function () {
    var Me = {
        Initialize: function () {

            //Load conventional (or any) html file onto a built-in UI object
            Apps.LoadTemplate('Login', '/Scripts/Apps/Components/Main/Login/Login.html', function () {

                //Load conventional (or any) css file
                Apps.LoadStyle('/Scripts/Apps/Components/Main/Login/Login.css');

                //Puts html file (including any text/templates) on DOM
                Apps.UI.Login.Drop(); 

                //Both drops and shows
                Apps.UI.Login.Show(); 

                Apps.Notify('info', 'Login component initialized.');

                //Note that you have access to all components anywhere.
                //You can wait for "ComponentsReady" event or load on demand w/callback using "Apps.LoadComponent"
                //For access to this component from inside this component you can use "Me"
                //From DOM always use full namespace
                let userCount = Apps.Components.Main.GetUserCount();

                Apps.Notify('success', 'There are ' + userCount + ' users logged in.');

                //Drop in login controls
                if (!Me.IsCurrentlyLoggedIn()) {
                    let greeting = 'Welcome to my AppsJS demo website!';
                    let buttonText = 'Login To Get Started. Not.';
                    let loginHtml = Apps.Util.GetHTML('Main_Login_LoginPrompt_Template', [greeting, buttonText]);
                    //"Main_Login_Container" is on index.html in this case
                    $('#Main_Login_Container').html(loginHtml);
                }
            });
        },
        IsCurrentlyLoggedIn: function () {
            return false;
        },
        LogMeIn: function () {
            Apps.Notify('error', 'Unauthorized attempt unsuccessful.');
        }
    };
    return Me;
});