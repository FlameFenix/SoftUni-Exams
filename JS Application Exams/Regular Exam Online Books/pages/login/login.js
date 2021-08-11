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

async function submitHandler(e) {
    e.preventDefault();
    let formData = new FormData(e.target);

    let email = formData.get('email');
    if (email.trim() === '') {
        window.alert('Email is required');
    }

    let password = formData.get('password');
    if (password.trim() === '') {
        window.alert('Password is required');
    }

    let user = {
        email,
        password
    }

    let loginResult = await _authService.login(user);
    _router.redirect('/dashboard');
}

async function getView(context) {
    _form = {
        submitHandler,
        errorMessages: []
    }
    let templateResult = loginTemplate(_form);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}