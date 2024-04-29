import {NavLink} from 'react-router-dom'

export default function () {

    return <nav id="Navbar">
        <NavLink to="/">Produkter</NavLink>
        <NavLink to="/about">Om oss</NavLink>
    </nav>
}
