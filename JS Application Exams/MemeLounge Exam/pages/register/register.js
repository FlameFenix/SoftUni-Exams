import { registerTemplate } from "./registerTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _authService = undefined;
let _form = undefined;

function initialize(router, renderHandler, authService) {
    _router = router;
    _renderHandler = renderHandler;
    _authService = authService;
}

async function submitHandler(e){
    e.preventDefault();
    try{
        let formData = new FormData(e.target);

        let emptyFields = false;

        let email = formData.get('email');
        if(email.trim() === ''){
            window.alert('Email is required');
            emptyFields = true;
        }

        let password = formData.get('password');
        if(password.trim() === ''){
            window.alert('Password is required');
            emptyFields = true;
        }

        let username = formData.get('username');
        if(username.trim() === ''){
            window.alert('Username is required');
            emptyFields = true;
        }

        let repeatPassword = formData.get('repeatPass');
        if(repeatPassword.trim() === ''){
            window.alert('Repeat Password is required');
            emptyFields = true;
        }

        let gender = formData.get('gender');
        if(gender.trim() === ''){
            window.alert('Gender is required');
            emptyFields = true;
        }

        if(emptyFields) {
            return;
        }
        let user = {
            email,
            password,
            username,
            gender
        }
    
        await _authService.register(user);
        _router.redirect('/all-memes');
    } catch (err){
        alert(err);
    }
   
}

async function getView(context) {
    _form = {
        submitHandler,
    }
    let templateResult = registerTemplate(_form);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}