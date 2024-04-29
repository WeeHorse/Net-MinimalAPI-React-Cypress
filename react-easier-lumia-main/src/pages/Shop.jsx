import {useStates} from 'react-easier'
import Products from "../components/Products.jsx";

export default function () {

    const search = useStates({text: ''});

    return <>
        <input type="text" id="search" placeholder="Type to search" {...search.bind('text')} />
        <Products search={search.text}/>        
    </>
}
