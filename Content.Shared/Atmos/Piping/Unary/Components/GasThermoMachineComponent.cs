using Content.Shared.Atmos;
using Content.Shared.Guidebook;
using Robust.Shared.GameStates;

namespace Content.Shared.Atmos.Piping.Unary.Components
{
    [RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true)]
    public sealed partial class GasThermoMachineComponent : Component
    {
        [DataField("inlet")]
        public string InletName = "pipe";

        /// <summary>
        ///     Current electrical power consumption, in watts. Increasing power increases the ability of the
        ///     thermomachine to heat or cool air.
        /// </summary>
        [DataField]
        public float HeatCapacity = 5000;

        [DataField, AutoNetworkedField]
        public float TargetTemperature = Atmospherics.T20C;

        /// <summary>
        ///     Tolerance for temperature setpoint hysteresis.
        /// </summary>
        [DataField, ViewVariables(VVAccess.ReadOnly)]
        public float TemperatureTolerance = 2f;

        /// <summary>
        ///     Implements setpoint hysteresis to prevent heater from rapidly cycling on and off at setpoint.
        ///     If true, add Sign(Cp)*TemperatureTolerance to the temperature setpoint.
        /// </summary>
        [ViewVariables(VVAccess.ReadOnly)]
        public bool HysteresisState;

        /// <summary>
        ///     Coefficient of performance. Output power / input power.
        ///     Positive for heaters, negative for freezers.
        /// </summary>
        [DataField("coefficientOfPerformance")]
        public float Cp = 0.9f; // output power / input power, positive is heat

        /// <summary>
        ///     Current minimum temperature
        ///     Ignored if heater.
        /// </summary>
        [DataField, AutoNetworkedField]
        public float MinTemperature = 73.15f;

        /// <summary>
        ///     Current maximum temperature
        ///     Ignored if freezer.
        /// </summary>
        [DataField, AutoNetworkedField]
        public float MaxTemperature = 593.15f;

        /// <summary>
        /// Last amount of energy added/removed from the attached pipe network
        /// </summary>
        [DataField]
        public float LastEnergyDelta;

        /// <summary>
        /// An percentage of the energy change that is leaked into the surrounding environment rather than the inlet pipe.
        /// </summary>
        [DataField]
       	public float EnergyLeakPercentage;

        /// <summary>
        /// If true, heat is exclusively exchanged with the local atmosphere instead of the inlet pipe air
        /// </summary>
        [DataField]
        public bool Atmospheric;
    }
}
