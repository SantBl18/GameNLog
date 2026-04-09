import type { GeneralFilter } from "../lib/types";
import Select from "react-select";

interface FilterSelectProps{
    options: GeneralFilter[];
    placeholder: string;
    onChange: (id: string | null) => void;
}

export default function FilterSelect({ options, placeholder, onChange} : FilterSelectProps){
    return (
        <Select
            options={options.map(o => ({value: o.id, label: o.name}))}
            onChange={(option) => onChange(option?.value ?? null)}
            isClearable
            placeholder={placeholder}
        />
    );
}