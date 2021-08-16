import { loginTemplate } from "./loginTemplate.js";

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

        let emptyField = false;

        let email = formData.get('email');
        if(email.trim() === ''){
            window.alert('Email is required');
            emptyField = true;
        }

        let password = formData.get('password');
        if(password.trim() === ''){
            window.alert('Password is required');
            emptyField = true;
        }

        if(emptyField) {
            return;
        }

        let user = {
            email,
            password
        }
    
        let loginResult = await _authService.login(user);
        _router.redirect('/all-memes');
    } catch (err){
        // alert(err);
    }
   
}

async function getView(context) {
    _form = {
        submitHandler,
    }
    let templateResult = loginTemplate(_form);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}