import {LitElement,css, html} from 'https://cdn.jsdelivr.net/gh/lit/dist@3/core/lit-core.min.js';
export class MyDialog extends LitElement {
  static properties = {
    title: {type:String},
    open: {type:Boolean},
  };
  // Define scoped styles right with your component, in plain CSS
  static styles = css`      
  `;
  
  constructor() {
    super();
    // Declare reactive properties
    this.title = '';
    this.open = false;
  }

  // Render the UI as a function of component state
  render() {
    return html`
        <dialog ?open=${this.open}>
            <h3 style="margin-top:-10px">${this.title}</h3>    
            <hr style="margin-top:-10px" />
            <slot></slot> 
            <div style='width:100%;text-align:right'>
                <input @click=${this._closeDialog} type="button" value="關閉上傳視窗" />
            </div>
        </dialog>    
    `;
  }

  _closeDialog(){
    this.open = false;
  }
}
customElements.define('my-dialog', MyDialog);