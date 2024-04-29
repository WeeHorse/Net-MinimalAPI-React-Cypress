import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given('I open the site', () => {
  cy.visit('/')
});

Then('I am on the site', () => {
  cy.get('h1').should('contain', 'Lumia')
});