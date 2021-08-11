import { editTemplate } from "./editTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _booksService = undefined;
let _form = undefined;

function initialize(router, renderHandler, booksService) {
    _router = router;
    _renderHandler = renderHandler;
    _booksService = booksService;
}

async function submitHandler(id, e) {
    e.preventDefault();
    try {
        let formData = new FormData(e.target);
        
        let isThereEmptyField = false;

        let title = formData.get('title');
        if (title.trim() === '') {
            window.alert('Title is required');
            isThereEmptyField = true;
        }

        let description = formData.get('description');
        if (description.trim() === '') {
            window.alert('Description is required');
            isThereEmptyField = true;
        }

        let imageUrl = formData.get('imageUrl');
        if (imageUrl.trim() === '') {
            window.alert('Image Url is required');
            isThereEmptyField = true;
        }

        let type = formData.get('type');
        if (imageUrl.trim() === '') {
            window.alert('Type is required');
            isThereEmptyField = true;
        }

        if (isThereEmptyField) {
            return;
        }

        let book = {
            title,
            description,
            imageUrl,
            type
        }

        await _booksService.update(book, id);
        _router.redirect(`/details/${id}`);
    } catch (err) {
        alert(err);
    }

}

async function getView(context) {
    let id = context.params.bookId;
    let book = await _booksService.get(id);

    _form = {
        submitHandler,
        errorMessages: [],
        book
    }
    let templateResult = editTemplate(_form);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}