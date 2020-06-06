using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

// Notes
// 1. What a finite state machine is
// 2. Examples where you'd use one
//     AI, Animation, Game State
// 3. Parts of a State Machine
//     States & Transitions
// 4. States - 3 Parts
//     Tick - Why it's not Update()
//     OnEnter / OnExit (setup & cleanup)
// 5. Transitions
//     Separated from states so they can be re-used
//     Easy transitions from any state

public class StateMachine
{
   private IState _currentState;
   
   private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type,List<Transition>>();
   private List<Transition> _currentTransitions = new List<Transition>();
   private List<Transition> _anyTransitions = new List<Transition>();
   
   private static List<Transition> EmptyTransitions = new List<Transition>(0);
    
    /// <summary>
    /// Metodo que chequea si se cumple alguna condicion para realizar una transicion
    /// </summary>
   public void Tick()
   {
      var transition = GetTransition();
        if (transition != null)
         SetState(transition.To);
      
      _currentState?.Tick();
   }

    /// <summary>
    /// Pasa al estado específico directamente. Es equivalente al método Play() en el animator.
    /// </summary>
    /// <param name="state"></param>
   public void SetState(IState state)
   {
        //Si el estado recibido es el mismo que el actual, entonces no hace nada, vuelve.
      if (state == _currentState)
         return;
      
      //Antes de cambiar el estado, ejecuta el método OnExit para setear todos los eventos necesarios antes de pasar al siguiente estado. Luego setea el estado mismo al actual.
      _currentState?.OnExit();
      _currentState = state;
      
      //Busca si existe dicha transición. Sino, simplemente borra en memoria el estado nulo.
      _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
      if (_currentTransitions == null)
         _currentTransitions = EmptyTransitions;
      
      //Ejecuta el metodo OnEnter para triggerear todos los eventos necesarios antes de operar en el mismo estado.
      _currentState.OnEnter();
   }

    /// <summary>
    /// Agrega una transicion desde un estado especifico a otro pasando por parámetro la condición a cumplir.
    /// </summary>
    /// <param name="from">Estado inicial</param>
    /// <param name="to">Estado siguiente</param>
    /// <param name="predicate">Conidicion a cumplir para que se realice la transicion a ese estado</param>
   public void AddTransition(IState from, IState to, Func<bool> predicate)
   {
      if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
      {
         transitions = new List<Transition>();
         _transitions[from.GetType()] = transitions;
      }
      
      transitions.Add(new Transition(to, predicate));
    }

    /// <summary>
    /// Agrega una transicion desde cualquier estado al estado especifico pasando por parámetro la condición a cumplir
    /// </summary>
    /// <param name="state">Estado siguiente</param>
    /// <param name="predicate">Condición a cumplir para que se realice la transición a ese estado</param>
   public void AddAnyTransition(IState state, Func<bool> predicate)
   {
      _anyTransitions.Add(new Transition(state, predicate));
   }

   private class Transition
   {
      public Func<bool> Condition {get; }
      public IState To { get; }

      public Transition(IState to, Func<bool> condition)
      {
         To = to;
         Condition = condition;
      }
   }

   private Transition GetTransition()
   {
      foreach(var transition in _anyTransitions)
         if (transition.Condition())
            return transition;
      
      foreach (var transition in _currentTransitions)
         if (transition.Condition())
            return transition;

      return null;
   }
}