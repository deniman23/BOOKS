import { useEffect, useState } from "react"
import ModalButton from "./ModalButton"
import "./Book.css";
import { FaSearch } from "react-icons/fa";
import "./SearchBar.css";

const URL = "/api/books"
const genresURL = "/api/books/genres"
const authorsURL = "/api/books/authors"

const Books = () => {
    const [allBooks, setBooks] = useState([])
    const [allGenres, setGenres] = useState([])
    const [allAuthors, setAuthors] = useState([])
    const [selectedGenres, setSelectedGenres] = useState([])
    const [selectedAuthors, setSelectedAuthors] = useState([])

    //получение списка всех жанров
    const getGenres = async () => {
        const options = {
            method: "GET",
            headers: new Headers()
        }
        const result = await fetch(genresURL, options)
        if (result.ok){
            const genres = await result.json()
            setGenres(genres)
            return genres
        }
        return []
    }

    //получение списка всех авторов
    const getAuthors = async () => {
        const options = {
            method: "GET",
            headers: new Headers()
        }
        const result = await fetch(authorsURL, options)
        if (result.ok){
            const authors = await result.json()
            setAuthors(authors)
            return authors
        }
        return []
    }

    //получение всех книг
    const getBooks = async () => {
        const options = {
            method: "GET",
            headers: new Headers()
        }
        const result = await fetch(URL, options)
        if (result.ok){
            const books = await result.json()
            setBooks(books)
            return books
        }
        return []
    }

    //добавление книги
    const addBook = async () => {
        const titleFromInput = document.querySelector('#title').value
        const descriptionFromInput = document.querySelector('#description').value

        if (titleFromInput.trim().length === 0){
            alert('Введите название книги')
            return
        }
        if (descriptionFromInput.trim().length === 0){
            alert('Введите описание книги')
            return
        }
        if (selectedAuthors.length === 0){
            alert('Выберите хотя бы одного автора')
            return
        }

        if (selectedGenres.length === 0){
            alert('Выберите хотя бы один жанр')
            return
        }

        const newBook = {
            title: titleFromInput.trim(),
            description: descriptionFromInput.trim(),
            authors: selectedAuthors,
            genres: selectedGenres
        }


        const headers = new Headers()
        headers.set('Content-Type', 'application/json')

        const options = {
            method: "POST",
            headers: headers,
            body: JSON.stringify(newBook)
        }

        const result = await fetch(URL, options)
        if (result.ok){
            const book = await result.json()
            allBooks.push(book)
            setBooks(allBooks.slice())
        }
    }

    //удаление книги
    const deleteBook = (id) => {
        const options = {
            method: "DELETE",
            headers: new Headers()
        }
        fetch(URL + `/${id}`, options)

        setBooks(allBooks.filter(x => x.id !== id))
    }

    //обновление информации о книге
    const updateBook = async (oldBook) => {
        const headers = new Headers()
        headers.set('Content-Type', 'application/json')

        const options = {
            method: "PATCH",
            headers: headers,
            body: JSON.stringify(oldBook)
        }

        const result = await fetch(URL, options)
        if (result.ok){
            const book = await result.json()
            const updatedBook = allBooks.find(x => x.id === oldBook.id)
            allBooks[updatedBook] = book
            setBooks(allBooks.slice())
        }
    }


    
    const authorClick = (target, author) => {
        if (target.classList.contains('active')){
            target.classList.remove('active')
            setSelectedAuthors(selectedAuthors.filter(x => x.id !== author.id))
        }
        else{
            target.classList.add('active')
            selectedAuthors.push(author)
            setSelectedAuthors(selectedAuthors.slice())
        }
    }

    const genreClick = (target, genre) => {
        if (target.classList.contains('active')){
            target.classList.remove('active')
            setSelectedGenres(selectedGenres.filter(x => x.id !== genre.id))
        }
        else{
            target.classList.add('active')
            selectedGenres.push(genre)
            setSelectedGenres(selectedGenres.slice())
        }
    }
 
    useEffect(() => {
        getBooks()
        getGenres()
        getAuthors()
    }, [])

    return (
        <div>
            <div>
                <h4>Добавить книгу</h4>
                <div >
                    <input id="title" type="text" className="input-wrapper" placeholder="Название"></input>
                </div>
                <div >
                    <textarea id="description" className="input-wrapper" placeholder="Описание"/>
                </div>

                <div className="authors-list">
                    <h3>Выберите авторов</h3>
                    {allAuthors.map(x => <AuthorItem 
                                                key={x.id} 
                                                author={x} 
                                                selectAction={authorClick}
                                                />)}
                </div>
                <br></br>
                <div className="genres-list">
                    <h3>Выберите жанры</h3>
                    {allGenres.map(x => <GenreItem 
                                                key={x.id} 
                                                genre={x} 
                                                selectAction={genreClick}
                                                />)}
                </div>
                <div>
                    <button id='addBookButton' onClick={() => addBook()}>Добавить книгу</button>
                </div>
                
            </div>
            <hr></hr>
            <div  style={{display: 'flex'}}>
                <div style={{marginRight: '10px'}}><h3>Книги</h3></div>
                <SearchBar 
                getBooks={getBooks}
                setResults={setBooks}/>
            </div>
            
            
            <div>
                {allBooks.map(x => <BookItem 
                                            key={x.id} 
                                            book={x} 
                                            deleteAction={deleteBook}
                                            updateAction={updateBook}
                                            />)}
            </div>
        </div>  
    )
}


export default Books; 

const BookItem = ({book, deleteAction, updateAction}) => {
    return (
        <div style={{margin: "5px", backgroundColor:"whitesmoke", padding: "10px", borderRadius: "10px"}}>
            <h3>{book.title}</h3>
            <p>{book.description}</p>
            <div className="authors-list">
                <h5>Авторы</h5>
                {book.authors?.map(x => <AuthorItem 
                                            key={x.id} 
                                            author={x} 
                                            selectAction={() => {}}
                                            />)}
            </div>
            <br></br>
            <div className="genres-list">
                <h5>Жанры</h5>
                {book.genres?.map(x => <GenreItem 
                                            key={x.id} 
                                            genre={x} 
                                            selectAction={() => {}}
                                            />)}
            </div>
            <div style={{display: 'flex'}}>
                <button className='book-button' onClick={() => deleteAction(book.id)} >Удалить</button>
                <ModalButton 
                    btnName={'Обновить'} 
                    title='Обновить книгу'
                    modalContent={
                        <div>
                            <div style={{margin: "10px"}}>
                                <input 
                                    className="input-wrapper"
                                    id="title" 
                                    type="text" 
                                    defaultValue={book.title}
                                    onChange={e => book.title = e.target.value.trim()}
                                />
                            </div>
                            <div style={{margin: "10px"}}>
                                <textarea 
                                    className="input-wrapper"
                                    id="description" 
                                    defaultValue={book.description}
                                    onChange={e => book.description = e.target.value.trim()}
                                />
                            </div>
                            <button 
                                id="update-button"
                                onClick={() => updateAction(book)} 
                                style={{marginLeft: "10px", border: "none", padding: "7.5px", borderRadius: "5px"}}
                            >Обновить</button>
                        </div>
                    }
                    />
            </div>
        </div>
    )
}


const AuthorItem = ({ author, selectAction}) => {
    return(
        <div className="author-item" onClick={e => selectAction(e.target, author)}>
            {author.name + ' ' + author.surname}
        </div>
    )
}

const GenreItem = ({genre, selectAction}) => {
    return(
        <div className="genre-item" onClick={e => selectAction(e.target, genre)}>
            {genre.name}
        </div>
    )
}

//строка поиска и отправка запроса на поиск
const SearchBar = ({ getBooks, setResults }) => {
    const [input, setInput] = useState("");
    const fetchData = async (value) => {
        if (value.trim() === ""){
            getBooks()
            return
        }
        const URL = "/api/books/search=" + value
        setResults([])
        const result = await fetch(URL)
        if (result.ok){
            const books = await result.json()
            setResults(books)
        }
    };
  
    const handleChange = (value) => {
      setInput(value);
    };
  
    return (
      <div className="input-wrapper">
        <FaSearch id="search-icon" />
        <input
          placeholder="Введите название книги, жанр или автора"
          value={input}
          onChange={(e) => handleChange(e.target.value)}
        />
        <button id="search-button" onClick={() => fetchData(input)}>Найти</button>
      </div>
    );
  };