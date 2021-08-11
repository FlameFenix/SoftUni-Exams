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

async function submitHandler(e) {
    e.preventDefault();
    let formData = new FormData(e.target);

    try {
        let isThereEmptyField = false;

        let email = formData.get('email');
        if (email.trim() === '') {
            window.alert('Email is required');
            isThereEmptyField = true;
        }

        let password = formData.get('password');
        if (password.trim() === '') {
            window.alert('Password is required');
            isThereEmptyField = true;
        }

        let repeatPassword = formData.get('confirm-pass');
        if (repeatPassword.trim() === '') {
            window.alert('Repeat Password is required');
            isThereEmptyField = true;
        }

        if(isThereEmptyField) {
            return;
        }

        let user = {
            email,
            password
        }
    
        await _authService.register(user);
        _router.redirect('/dashboard');
    
    } catch (err){
        //alert(err);
    }


 

}

async function getView(context) {
    _form = {
        submitHandler,
        errorMessages: []
    }
    let templateResult = registerTemplate(_form);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}