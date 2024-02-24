
const formErrorHandler = (e, validationResult) => {

    let spanElement = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)

    console.log(spanElement)

    if (validationResult) {
        e.target.classList.remove('input-validation-error')
        e.target.classList.add('input-validation-valid')
        spanElement.classList.remove('input-validation-error')
        spanElement.classList.add('input-validation-valid')
        spanElement.innerHTML = '<i class="fa-solid fa-check"></i>'
    }
    else {
        e.target.classList.add('input-validation-error')
        e.target.classList.remove('input-validation-valid')
        spanElement.classList.add('input-validation-error')
        spanElement.classList.remove('input-validation-valid')
        spanElement.innerHTML = e.target.dataset.valRequired
    }
}

const lengthValidator = (value, minLength = 2) => {
    if (value.length >= minLength)
        return true

    else return false
}

const compareValue = (value, compareValue) => {

    if (value === compareValue)
        return true;

    else return false;
}

const emailValidator = (value) => {
    const regex = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (regex.test(value))
        return true

    return false;
}


const textValidation = (e) => {
    formErrorHandler(e, lengthValidator(e.target.value))
}
const emailValidation = (e) => {
    formErrorHandler(e, emailValidator(e.target.value))
}

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {

        input.addEventListener('blur', e => {

            switch (e.target.type) {

                case 'text':
                    textValidation(e)
                    break;
                case 'email':
                    emailValidation(e)
                    break;
            }
        })
    }
})