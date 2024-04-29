import {useStates, useFetch, useAutoKeys, useDebug} from 'react-easier'
import {Outlet} from 'react-router-dom';
import Navbar from "./components/Navbar.jsx";
import Cart from "./components/Cart.jsx";
export default function App() {  
    useAutoKeys()
    // useDebug()
    
    // Create our states
    useStates('products', useFetch('/products.json'))

    const cart = useStates('cart', {
        items: [],
        total: 0
    })

    // Application DOM
    return <>
            <main className="corners padding transp">
                <h1>Lumia</h1>
                <Navbar/>
                <Outlet/> {/* sub routes from router defined in main */}
            </main>
            <Cart/>
        </>
}
