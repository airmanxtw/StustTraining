import {LitElement, css,html} from 'https://cdn.jsdelivr.net/gh/lit/dist@3/core/lit-core.min.js';
export class HelloWorld extends LitElement {
  static properties = {
    name: {type:String},
  };
  // Define scoped styles right with your component, in plain CSS
    static styles = css`
    :host {
        color:red;
    }
  `;

  constructor() {
    super();
    // Declare reactive properties
    this.name = 'World';
  }

  // Render the UI as a function of component state
  render() {
    return html`<div>
                <h2>${this.name}</h2>
                <slot></slot>
                </div>`;
  }
}
customElements.define('hello-world', HelloWorld);