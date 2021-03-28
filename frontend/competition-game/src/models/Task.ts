import {SampleCode} from './SampleCode';

export interface Task {
    id: number;
    name: string;
    description: string;
    sampleCodes: SampleCode[];
}

