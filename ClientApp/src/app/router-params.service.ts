import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';

export interface IStateParams {
    clientId?: number;
    depositId?: number;
    creditId?: number;
}

@Injectable({
    providedIn: 'root'
})
export class RouterParamsService {

    constructor(private route: ActivatedRoute) {
    }

    public get clientId(): number | undefined {
        return this.params.clientId;
    }

    public get creditId(): number | undefined {
        return this.params.creditId;
    }

    public get depositId(): number | undefined {
        return this.params.depositId;
    }

    public get params(): IStateParams {
        return this.getParamsFromRoutes(this.getAllRouteSnapshots(this.route.snapshot));
    }

    private getParamsFromRoutes(routeSnapshots: ActivatedRouteSnapshot[]): IStateParams {
        const params = routeSnapshots
            .filter(p => p != null)
            .reduce((a, b) => ({
                ...a,
                ...b.params
            }), {} as IStateParams);

        params.clientId = this.resolveAsNumber(params.clientId);
        params.depositId = this.resolveAsNumber(params.depositId);
        params.creditId = this.resolveAsNumber(params.creditId);

        return params;
    }

    private resolveAsNumber(value: unknown): number | undefined {
        if (value === undefined) {
            return undefined;
        }

        const result = Number(value);
        if (isNaN(result)) {
            throw new Error('Route param cannot be resolved as number');
        }
        return result;
    }

    private getAllRouteSnapshots(route: ActivatedRouteSnapshot): ActivatedRouteSnapshot[] {
        if (route.children.length === 0) {
            return [route];
        }
        const routes = route.children.map((childRoute: ActivatedRouteSnapshot) =>
            this.getAllRouteSnapshots(childRoute));
        if (routes.length === 0) {
            return [];
        }
        return routes.reduce((a, b) => a.concat(b), [route]);
    }
}
