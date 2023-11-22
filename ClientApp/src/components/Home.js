import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h2>Это каталог книг</h2>
        <h3>Необходимый функционал вы найдёте в разделе Книги</h3>
      </div>
    );
  }
}
