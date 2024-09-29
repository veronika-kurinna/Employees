
const phonePattern = /^\+[1-9]\d{1,14}$/;

export const phoneValidation = (value) =>  {
    return phonePattern.test(value) || 'Incorrect phone number';
};