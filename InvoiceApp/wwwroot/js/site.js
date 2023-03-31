// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    setTimeout(function () {
        $("#notification").fadeOut("slow", function () {
            $(this).remove();
        });
    }, 5000); // 5 seconds
});

$(function () {
    // Handle tab click event
    $('.nav-tabs a').click(function (e) {
        e.preventDefault();
        $(this).tab('show'); 
        // Set active class to the clicked tab
        $('.nav-tabs li').removeClass('active');
        $(this).parent().addClass('active');
        // Show corresponding tab-pane
        var target = $(this).attr('href');
        $('.tab-pane').removeClass('active');
        $(target).addClass('active');
    });
    // Set initial active tab
    $('.nav-tabs li:first-child').addClass('active');
    $('.tab-pane:first-child').addClass('active');
});


const form = document.querySelector('form');
const nameInput = form.querySelector('#nameInput');
const address1Input = form.querySelector('#address1');
const cityInput = form.querySelector('#city');
const provinceInput = form.querySelector('#province');
const zipInput = form.querySelector('#postal-code');
const phoneInput = form.querySelector('#phone');
const emailInput = form.querySelector('#email');

 //Add a submit event listener to the form
const form1 = document.querySelector('form');
const submitButton = form.querySelector('button[type="submit"]');
submitButton.addEventListener('click', function (event) {
    event.preventDefault();

   //  Validate the fields
    let isValid = true;
    isValid &= validateRequiredField(nameInput);
    isValid &= validateRequiredField(address1Input);
    isValid &= validateRequiredField(cityInput);
    isValid &= validateRequiredField(provinceInput);
    isValid &= validateRequiredField(zipInput);
    isValid &= validateRequiredField(phoneInput,);
    isValid &= validateEmail(emailInput);

    // Check if any validation errors occurred
    if (!isValid) {
       //  Display a generic error message above the form
        const errorMessage = document.createElement('div');
        errorMessage.classList.add('error-message');
        errorMessage.innerText = 'Please correct the errors in the form and try again.';
        form.insertBefore(errorMessage, form.firstChild);

       //  Highlight the fields with errors
        if (!nameInput.value) highlightField(nameInput, 'Name is required.');
        if (!address1Input.value) highlightField(address1Input, 'Address 1 is required.');
        if (!cityInput.value) highlightField(cityInput, 'City is required.');
        if (!provinceInput.value) highlightField(provinceInput, 'Province/State is required.');
        if (!zipInput.value) highlightField(zipInput, 'Zip/Postal is required.');
        if (!phoneInput.value) highlightField(phoneInput, 'Phone is required.');
        if (!provinceInput.value || !validateProvince(provinceInput)) highlightField(provinceInput, 'Province/State must be a 2-letter state code.');
        if (!zipInput.value || !validateZip(zipInput)) highlightField(zipInput, 'Zip/Postal code must be a valid US zip or Canadian postal code.');
        if (!phoneInput.value || !validatePhone(phoneInput)) highlightField(phoneInput, 'Phone must be in a valid US or Canadian phone number format.');
        if (!emailInput.value || !validateEmail(emailInput)) highlightField(emailInput, 'Contact email must be in a valid email format.');
        
    } else {
       //  Process the form data
        console.log('Form data is valid. Processing...');

         //Remove any previous error messages and highlights
        const errorMessages = form.querySelectorAll('.error-message');
        errorMessages.forEach(errorMessage => errorMessage.remove());
        removeHighlights();
        event.target.form.submit();
    }
});

function validateRequiredField(input) {
    if (!input.value) {
        return false;
    }
    return true;
}

function validateProvince(input) {
    if (!input.value.match(/^[a-zA-Z]{2}$/i)) {
        return false;
    }
    return true;
}

function validateZip(input) {
    if (!input.value.match(/^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$/)) {
        return false;
    }
    return true;
}

function validatePhone(input) {
    if (!input.value.match(/^(\d{3})[\-]\d{3}[\-]\d{4}$|^(\d{10})$/)) {
        return false;
    }
    return true;
}
//validate email
function validateEmail(input) {
    if(!input.value.match(/^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$/)){
        return false;
    }
    return true;
}



function highlightField(input, errorMessage) {
     //Create a field-specific error message
    const errorElement = document.createElement('span');
    errorElement.classList.add('error-message');
    errorElement.innerText = errorMessage;

     //Add a red border to the input
    input.style.borderColor = 'red';

     //Add a different background color to the input
    input.style.backgroundColor = '#FEECEC';

     //Add the error message after the input
    input.insertAdjacentElement('afterend', errorElement);
}

function removeHighlights() {
     //Reset the border color and background color for all inputs
    const inputs = form.querySelectorAll('input');
    inputs.forEach(input => {
        input.style.borderColor = '';
        input.style.backgroundColor = '';
    });

    // Remove any previous error messages
    const errorMessages = form.querySelectorAll('.error-message');
    errorMessages.forEach(errorMessage => errorMessage.remove());
}



/*const form = document.querySelector('form');*/
const errorMessage = document.querySelector('#error-message');
// const Address = document.querySelector('#address1');

form.addEventListener('input', () => {
    let isValid = true;


    // Check phone field
    const phone = document.querySelector('#phone');
    phone.addEventListener('blur', () => {
        if (!/^(\d{3}-\d{3}-\d{4})$/.test(phone.value)) {
            phone.classList.add('error');
            phone.nextElementSibling.innerHTML = 'Phone must be in the format xxx-xxx-xxxx.';
            isValid = false;
        } else {
            phone.classList.remove('error');
            phone.nextElementSibling.innerHTML = '';
        }
    });


    // Check for duplicate phone numbers
    const phoneNumbers = Array.from(document.querySelectorAll('input[type="tel"]'));
    const duplicatePhones = phoneNumbers.filter((phone, index) =>
        phone.value !== '' && phoneNumbers.findIndex(p => p.value === phone.value) !== index
    );
    duplicatePhones.forEach(phone => {
        phone.classList.add('error');
        phone.nextElementSibling.innerHTML = 'Phone number is already in use.';
        isValid = false;
    });

    // Show error message if form is invalid
    if (!isValid) {
        errorMessage.style.display = 'block';
    } else {
        errorMessage.style.display = 'none';
    }
}
);