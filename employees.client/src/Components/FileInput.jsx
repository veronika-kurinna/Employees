export function FileInput(props){
    return(
        <div class="mt-5 mb-3">
            <label for="formFile" class="form-label" style={{fontWeight: "bold"}}>Upload CSV file</label>
            <input class="form-control" type="file" id="formFile" onChange={props.function} accept=".csv"/>
        </div>
    )
}