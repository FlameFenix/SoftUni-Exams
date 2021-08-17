import { createTemplate } from "./createTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _listingsService = undefined;
let _form = undefined;
let _notification = undefined;

function initialize(router, renderHandler, listingsService, notification) {
    _router = router;
    _renderHandler = renderHandler;
    _listingsService = listingsService;
    _notification = notification;
}

async function submitHandler(e) {
    e.preventDefault();
    try {
        let formData = new FormData(e.target);
        _form.errorMessages = [];

        let brand = formData.get('brand');
        if (brand.trim() === '') {
            _form.errorMessages.push('Brand is required');
        }

        let model = formData.get('model');
        if (model.trim() === '') {
            _form.errorMessages.push('Model is required');
        }

        let description = formData.get('description');
        if (description.trim() === '') {
            _form.errorMessages.push('Description is required');
        }

        let year = formData.get('year');
        if (year.trim() === '') {
            _form.errorMessages.push('Year is required');
        }

        let imageUrl = formData.get('imageUrl');
        if (imageUrl.trim() === '') {
            _form.errorMessages.push('imageUrl is required');
        }

        let price = formData.get('price');
        if (price.trim() === '') {
            _form.errorMessages.push('Price is required');
        }

        if (_form.errorMessages.length > 0) {
            let templateResult = createTemplate(_form);
            _notification.createNotification(_form.errorMessages.join('\n'));
            // alert(_form.errorMessages.join('\n'));
            return _renderHandler(templateResult);
        }

        let listing = {
            brand,
            model,
            description,
            year: Number(year),
            imageUrl,
            price: Number(price)
        }

        let loginResult = await _listingsService.create(listing);
        _router.redirect('/listings');
    } catch (err) {
        alert(err);
    }

}

async function getView(context) {
    _form = {
        submitHandler,
        errorMessages: []
    }
    let templateResult = createTemplate(_form);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}